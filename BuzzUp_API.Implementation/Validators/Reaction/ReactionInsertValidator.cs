using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Reactions;
using BuzzUp_API.DataAccess;
using FluentValidation;

namespace BuzzUp_API.Implementation.Validators.Reaction
{
    public class ReactionInsertValidator : AbstractValidator<ReactionInsertDTO>
    {
        public ReactionInsertValidator(BuzzUpContext ctx, IApplicationActor actor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.PostId)
                .NotEmpty()
                .WithMessage("Post is required.")
                .Must(id => ctx.Posts.Any(p => p.Id == id && p.IsActive && p.DeletedAt == null && CanView(ctx, actor.Id, id)))
                .WithMessage("Post does not exist.");

            RuleFor(x => x.ReactionTypeId)
                .NotEmpty()
                .WithMessage("Reaction type is required.")
                .Must(id => ctx.ReactionTypes.Any(t => t.Id == id && t.IsActive && t.DeletedAt == null))
                .WithMessage("Reaction type does not exist.");
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
