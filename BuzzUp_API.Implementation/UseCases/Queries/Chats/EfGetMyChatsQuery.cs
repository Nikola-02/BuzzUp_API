using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Chats;
using BuzzUp_API.Application.UseCases.Queries.Chats;
using BuzzUp_API.DataAccess;

namespace BuzzUp_API.Implementation.UseCases.Queries.Chats
{
    public class EfGetMyChatsQuery : EfUseCase, IGetMyChatsQuery
    {
        private readonly IApplicationActor _actor;

        public EfGetMyChatsQuery(BuzzUpContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 32;

        public string Name => "Get My Chats";

        public List<ChatInboxItemDTO> Execute(ChatSearch search)
        {
            var actorUserId = _actor.Id;

            var actorChatIds = Context.UserChats
                .Where(actorMembership =>
                    actorMembership.UserId == actorUserId &&
                    actorMembership.IsActive &&
                    actorMembership.DeletedAt == null &&
                    actorMembership.Chat.IsActive &&
                    actorMembership.Chat.DeletedAt == null)
                .Select(actorMembership => actorMembership.ChatId);

            var inboxRows = Context.UserChats
                .Where(otherMembership =>
                    actorChatIds.Contains(otherMembership.ChatId) &&
                    otherMembership.UserId != actorUserId &&
                    otherMembership.IsActive &&
                    otherMembership.DeletedAt == null &&
                    otherMembership.User.IsActive &&
                    otherMembership.User.DeletedAt == null)
                .Select(otherMembership => new
                {
                    ChatId = otherMembership.ChatId,
                    ChatCreatedAt = otherMembership.Chat.CreatedAt,
                    OtherUser = new ChatOtherUserDTO
                    {
                        Id = otherMembership.User.Id,
                        FirstName = otherMembership.User.FirstName,
                        LastName = otherMembership.User.LastName,
                        Username = otherMembership.User.Username,
                        Image = otherMembership.User.Image
                    },
                    HasUnread = Context.Messages.Any(message =>
                        message.ChatId == otherMembership.ChatId &&
                        message.IsActive &&
                        message.DeletedAt == null &&
                        message.SenderId != actorUserId &&
                        !message.IsRead),
                    LastMessageAt = Context.Messages
                        .Where(message =>
                            message.ChatId == otherMembership.ChatId &&
                            message.IsActive &&
                            message.DeletedAt == null)
                        .Select(message => (DateTime?)message.CreatedAt)
                        .Max()
                })
                .ToList();

            return inboxRows
                .OrderByDescending(inboxRow => inboxRow.HasUnread)
                .ThenByDescending(inboxRow => inboxRow.LastMessageAt ?? inboxRow.ChatCreatedAt)
                .Select(inboxRow => new ChatInboxItemDTO
                {
                    Id = inboxRow.ChatId,
                    OtherUser = inboxRow.OtherUser,
                    HasUnread = inboxRow.HasUnread
                })
                .ToList();
        }
    }
}
