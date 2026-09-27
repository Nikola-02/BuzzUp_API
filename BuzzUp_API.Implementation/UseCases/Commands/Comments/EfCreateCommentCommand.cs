using AutoMapper;
using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Comments;
using BuzzUp_API.Application.UseCases.Commands.Comments;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Comments
{
    public class EfCreateCommentCommand : EfCreateUseCase<CommentInsertDTO, Comment>, ICreateCommentCommand
    {
        private readonly IApplicationActor _actor;

        public EfCreateCommentCommand(
            BuzzUpContext context,
            IMapper mapper,
            IValidator<CommentInsertDTO> validator,
            IApplicationActor actor)
            : base(context, mapper, validator)
        {
            _actor = actor;
        }

        public override int Id => 28;

        public override string Name => "Create Comment";

        protected override void AfterMap(CommentInsertDTO request, Comment entity)
        {
            entity.UserId = _actor.Id;
            entity.ParentId = request.ParentId;

            var recipientUserIds = new HashSet<int>();
            var postAuthorId = Context.Posts
                .Where(post => post.Id == request.PostId)
                .Select(post => post.UserId)
                .First();

            if (postAuthorId != _actor.Id)
            {
                recipientUserIds.Add(postAuthorId);
            }

            if (request.ParentId.HasValue)
            {
                var parentCommentAuthorId = Context.Comments
                    .Where(comment => comment.Id == request.ParentId.Value)
                    .Select(comment => comment.UserId)
                    .First();

                if (parentCommentAuthorId != _actor.Id)
                {
                    recipientUserIds.Add(parentCommentAuthorId);
                }
            }

            if (recipientUserIds.Count == 0)
            {
                return;
            }

            var commentNotificationTypeId = Context.NotificationTypes
                .Where(notificationType => notificationType.Name == "Comment" && notificationType.IsActive && notificationType.DeletedAt == null)
                .Select(notificationType => notificationType.Id)
                .First();

            foreach (var recipientUserId in recipientUserIds)
            {
                Context.Notifications.Add(new Notification
                {
                    RecipientUserId = recipientUserId,
                    ActorUserId = _actor.Id,
                    NotificationTypeId = commentNotificationTypeId,
                    PostId = request.PostId
                });
            }
        }
    }
}
