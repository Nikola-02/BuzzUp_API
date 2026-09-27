using BuzzUp_API.Application;
using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.UseCases.Commands.Comments;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases;

namespace BuzzUp_API.Implementation.UseCases.Commands.Comments
{
    public class EfDeleteCommentCommand : EfDeleteUseCase<Comment>, IDeleteCommentCommand
    {
        private readonly IApplicationActor _actor;

        public EfDeleteCommentCommand(BuzzUpContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public override int Id => 30;

        public override string Name => "Delete Comment";

        protected override void EnsureCanDelete(Comment entity)
        {
            var isCommentAuthor = entity.UserId == _actor.Id;
            var isPostAuthor = Context.Posts.Any(post =>
                post.Id == entity.PostId && post.UserId == _actor.Id);

            if (!isCommentAuthor && !isPostAuthor)
            {
                throw new ForbiddenException("You can only delete your own comment or a comment on your post.");
            }
        }

        protected override void AfterDelete(Comment entity)
        {
            SoftDeleteReplies(entity.Id);
        }

        private void SoftDeleteReplies(int parentCommentId)
        {
            var childComments = Context.Comments
                .Where(comment => comment.ParentId == parentCommentId && comment.IsActive && comment.DeletedAt == null)
                .ToList();

            foreach (var childComment in childComments)
            {
                childComment.IsActive = false;
                childComment.DeletedAt = DateTime.UtcNow;
                SoftDeleteReplies(childComment.Id);
            }
        }
    }
}
