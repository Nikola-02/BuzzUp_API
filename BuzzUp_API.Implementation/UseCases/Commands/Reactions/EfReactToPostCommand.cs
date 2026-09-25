using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Reactions;
using BuzzUp_API.Application.UseCases.Commands.Reactions;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.Validators.Reaction;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Reactions
{
    public class EfReactToPostCommand : EfUseCase, IReactToPostCommand
    {
        private readonly ReactionInsertValidator _validator;
        private readonly IApplicationActor _actor;

        public EfReactToPostCommand(
            BuzzUpContext context,
            ReactionInsertValidator validator,
            IApplicationActor actor)
            : base(context)
        {
            _validator = validator;
            _actor = actor;
        }

        public int Id => 26;

        public string Name => "React To Post";

        public void Execute(ReactionInsertDTO data)
        {
            _validator.ValidateAndThrow(data);

            var post = Context.Posts.First(p => p.Id == data.PostId);
            var existing = Context.Reactions
                .FirstOrDefault(r => r.PostId == data.PostId && r.UserId == _actor.Id);

            if (existing != null && existing.IsActive && existing.DeletedAt == null && existing.ReactionTypeId == data.ReactionTypeId)
            {
                Context.Reactions.Remove(existing);
                SyncReactionNotification(post.Id, post.UserId, null);
                Context.SaveChanges();
                return;
            }

            if (existing != null)
            {
                existing.ReactionTypeId = data.ReactionTypeId;
                existing.IsActive = true;
                existing.DeletedAt = null;
            }
            else
            {
                Context.Reactions.Add(new Reaction
                {
                    PostId = data.PostId,
                    UserId = _actor.Id,
                    ReactionTypeId = data.ReactionTypeId
                });
            }

            SyncReactionNotification(post.Id, post.UserId, data.ReactionTypeId);
            Context.SaveChanges();
        }

        private void SyncReactionNotification(int postId, int authorId, int? reactionTypeId)
        {
            var notificationTypeId = Context.NotificationTypes
                .Where(t => t.Name == "Reaction" && t.IsActive && t.DeletedAt == null)
                .Select(t => t.Id)
                .First();

            var rows = Context.Notifications
                .Where(n => n.IsActive && n.DeletedAt == null)
                .Where(n => n.NotificationTypeId == notificationTypeId)
                .Where(n => n.ActorUserId == _actor.Id && n.RecipientUserId == authorId && n.PostId == postId)
                .ToList();

            Context.Notifications.RemoveRange(rows);

            if (!reactionTypeId.HasValue || authorId == _actor.Id)
            {
                return;
            }

            Context.Notifications.Add(new Notification
            {
                RecipientUserId = authorId,
                ActorUserId = _actor.Id,
                NotificationTypeId = notificationTypeId,
                PostId = postId,
                ReactionTypeId = reactionTypeId
            });
        }
    }
}
