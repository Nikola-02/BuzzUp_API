using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Chats;
using BuzzUp_API.DataAccess;
using FluentValidation;

namespace BuzzUp_API.Implementation.Validators.Chat
{
    public class MessageInsertValidator : AbstractValidator<MessageInsertDTO>
    {
        public MessageInsertValidator(BuzzUpContext ctx, IApplicationActor actor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.ChatId)
                .NotEmpty()
                .WithMessage("Chat is required.")
                .Must(chatId => ActorIsInChat(ctx, actor.Id, chatId))
                .WithMessage("Chat does not exist.")
                .Must(chatId => AreAcceptedFriendsWithOtherUser(ctx, actor.Id, chatId))
                .WithMessage("You can only chat with friends.");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Message is required.")
                .MaximumLength(2000)
                .WithMessage("Message cannot be longer than 2000 characters.");
        }

        private static bool ActorIsInChat(BuzzUpContext ctx, int actorUserId, int chatId)
        {
            return ctx.UserChats.Any(membership =>
                membership.ChatId == chatId &&
                membership.UserId == actorUserId &&
                membership.IsActive &&
                membership.DeletedAt == null &&
                membership.Chat.IsActive &&
                membership.Chat.DeletedAt == null);
        }

        private static bool AreAcceptedFriendsWithOtherUser(BuzzUpContext ctx, int actorUserId, int chatId)
        {
            var otherUserId = ctx.UserChats
                .Where(membership =>
                    membership.ChatId == chatId &&
                    membership.UserId != actorUserId &&
                    membership.IsActive &&
                    membership.DeletedAt == null)
                .Select(membership => membership.UserId)
                .FirstOrDefault();

            if (otherUserId == 0)
            {
                return false;
            }

            return ctx.Friendships.Any(friendship =>
                friendship.IsActive &&
                friendship.DeletedAt == null &&
                friendship.FriendRequestStatus.Name == "Accepted" &&
                ((friendship.SenderUserId == actorUserId && friendship.ReceiverUserId == otherUserId) ||
                 (friendship.ReceiverUserId == actorUserId && friendship.SenderUserId == otherUserId)));
        }
    }
}
