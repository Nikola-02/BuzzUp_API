using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Chats;
using BuzzUp_API.DataAccess;
using FluentValidation;

namespace BuzzUp_API.Implementation.Validators.Chat
{
    public class ChatOpenValidator : AbstractValidator<ChatOpenDTO>
    {
        public ChatOpenValidator(BuzzUpContext ctx, IApplicationActor actor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User is required.")
                .Must(id => id != actor.Id)
                .WithMessage("You cannot open a chat with yourself.")
                .Must(id => ctx.Users.Any(u => u.Id == id && u.IsActive && u.DeletedAt == null))
                .WithMessage("User does not exist.")
                .Must(id => AreAcceptedFriends(ctx, actor.Id, id))
                .WithMessage("You can only chat with friends.");
        }

        private static bool AreAcceptedFriends(BuzzUpContext ctx, int actorUserId, int otherUserId)
        {
            return ctx.Friendships.Any(friendship =>
                friendship.IsActive &&
                friendship.DeletedAt == null &&
                friendship.FriendRequestStatus.Name == "Accepted" &&
                ((friendship.SenderUserId == actorUserId && friendship.ReceiverUserId == otherUserId) ||
                 (friendship.ReceiverUserId == actorUserId && friendship.SenderUserId == otherUserId)));
        }
    }
}
