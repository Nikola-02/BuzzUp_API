using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Friendships;
using BuzzUp_API.DataAccess;
using FluentValidation;

namespace BuzzUp_API.Implementation.Validators.Friendship
{
    public class FriendshipAcceptValidator : AbstractValidator<FriendshipInsertDTO>
    {
        public FriendshipAcceptValidator(BuzzUpContext ctx, IApplicationActor actor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User is required.")
                .Must(id => id != actor.Id)
                .WithMessage("You cannot accept a friend request from yourself.")
                .Must(id => ctx.Friendships.Any(f =>
                    f.IsActive &&
                    f.DeletedAt == null &&
                    f.FriendRequestStatus.Name == "Pending" &&
                    f.SenderUserId == id &&
                    f.ReceiverUserId == actor.Id))
                .WithMessage("Friend request does not exist.");
        }
    }
}
