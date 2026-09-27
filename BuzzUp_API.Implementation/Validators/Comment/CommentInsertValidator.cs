using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Comments;
using BuzzUp_API.DataAccess;
using FluentValidation;

namespace BuzzUp_API.Implementation.Validators.Comment
{
    public class CommentInsertValidator : AbstractValidator<CommentInsertDTO>
    {
        public CommentInsertValidator(BuzzUpContext ctx, IApplicationActor actor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.PostId)
                .NotEmpty()
                .WithMessage("Post is required.")
                .Must(id => ctx.Posts.Any(p => p.Id == id && p.IsActive && p.DeletedAt == null && CanView(ctx, actor.Id, id)))
                .WithMessage("Post does not exist.");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Comment is required.")
                .MaximumLength(2000)
                .WithMessage("Comment cannot be longer than 2000 characters.");

            RuleFor(x => x.ParentId)
                .Must((dto, parentId) =>
                    !parentId.HasValue ||
                    ctx.Comments.Any(comment =>
                        comment.Id == parentId.Value &&
                        comment.PostId == dto.PostId &&
                        comment.IsActive &&
                        comment.DeletedAt == null))
                .WithMessage("Parent comment does not exist.");
        }

        private static bool CanView(BuzzUpContext ctx, int actorId, int postId)
        {
            var post = ctx.Posts.FirstOrDefault(p => p.Id == postId && p.IsActive && p.DeletedAt == null);
            if (post == null)
            {
                return false;
            }

            if (post.UserId == actorId)
            {
                return true;
            }

            if (post.VisibilityType.Name == "Public")
            {
                return true;
            }

            if (post.VisibilityType.Name == "Friends")
            {
                return ctx.Friendships.Any(f =>
                    f.IsActive &&
                    f.DeletedAt == null &&
                    f.FriendRequestStatus.Name == "Accepted" &&
                    ((f.SenderUserId == actorId && f.ReceiverUserId == post.UserId) ||
                     (f.ReceiverUserId == actorId && f.SenderUserId == post.UserId)));
            }

            return false;
        }
    }
}
