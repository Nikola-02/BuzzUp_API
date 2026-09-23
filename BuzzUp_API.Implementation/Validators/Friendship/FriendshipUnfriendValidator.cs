using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Friendships;
using BuzzUp_API.DataAccess;
using FluentValidation;

namespace BuzzUp_API.Implementation.Validators.Friendship
{
    public class FriendshipUnfriendValidator : AbstractValidator<FriendshipInsertDTO>
    {
        public FriendshipUnfriendValidator(BuzzUpContext ctx, IApplicationActor actor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User is required.")
                .Must(id => id != actor.Id)
                .WithMessage("You cannot unfriend yourself.")
                .Must(id => ctx.Friendships.Any(f =>
                    f.IsActive &&
                    f.DeletedAt == null &&
                    f.FriendRequestStatus.Name == "Accepted" &&
                    ((f.SenderUserId == actor.Id && f.ReceiverUserId == id) ||
                     (f.ReceiverUserId == actor.Id && f.SenderUserId == id))))
                .WithMessage("Friendship does not exist.");
        }
    }
}
