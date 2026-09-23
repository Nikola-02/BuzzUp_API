using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Friendships;
using BuzzUp_API.DataAccess;
using FluentValidation;

namespace BuzzUp_API.Implementation.Validators.Friendship
{
    public class FriendshipInsertValidator : AbstractValidator<FriendshipInsertDTO>
    {
        public FriendshipInsertValidator(BuzzUpContext ctx, IApplicationActor actor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User is required.")
                .Must(id => id != actor.Id)
                .WithMessage("You cannot send a friend request to yourself.")
                .Must(id => ctx.Users.Any(u => u.Id == id && u.IsActive && u.DeletedAt == null))
                .WithMessage("User does not exist.")
                .Must(id => !HasActiveFriendship(ctx, actor.Id, id))
                .WithMessage("Friend request already exists.");
        }

        private static bool HasActiveFriendship(BuzzUpContext ctx, int actorId, int targetId)
        {
            return ctx.Friendships.Any(f =>
                f.IsActive &&
                f.DeletedAt == null &&
                ((f.SenderUserId == actorId && f.ReceiverUserId == targetId) ||
                 (f.SenderUserId == targetId && f.ReceiverUserId == actorId)));
        }
    }
}
