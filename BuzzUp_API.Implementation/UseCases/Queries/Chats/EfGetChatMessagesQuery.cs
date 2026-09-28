using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Chats;
using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.UseCases.Queries.Chats;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;

namespace BuzzUp_API.Implementation.UseCases.Queries.Chats
{
    public class EfGetChatMessagesQuery : EfUseCase, IGetChatMessagesQuery
    {
        private readonly IApplicationActor _actor;

        public EfGetChatMessagesQuery(BuzzUpContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 33;

        public string Name => "Get Chat Messages";

        public List<MessageDTO> Execute(int chatId)
        {
            var actorUserId = _actor.Id;

            var actorIsInChat = Context.UserChats.Any(membership =>
                membership.ChatId == chatId &&
                membership.UserId == actorUserId &&
                membership.IsActive &&
                membership.DeletedAt == null &&
                membership.Chat.IsActive &&
                membership.Chat.DeletedAt == null);

            if (!actorIsInChat)
            {
                throw new EntityNotFoundException(nameof(Chat), chatId);
            }

            var unreadIncomingMessages = Context.Messages
                .Where(message =>
                    message.ChatId == chatId &&
                    message.IsActive &&
                    message.DeletedAt == null &&
                    message.SenderId != actorUserId &&
                    !message.IsRead)
                .ToList();

            if (unreadIncomingMessages.Count > 0)
            {
                foreach (var unreadMessage in unreadIncomingMessages)
                {
                    unreadMessage.IsRead = true;
                }

                Context.SaveChanges();
            }

            return Context.Messages
                .Where(message =>
                    message.ChatId == chatId &&
                    message.IsActive &&
                    message.DeletedAt == null)
                .OrderBy(message => message.Id)
                .Select(message => new MessageDTO
                {
                    Id = message.Id,
                    Content = message.Content,
                    SenderId = message.SenderId,
                    IsMine = message.SenderId == actorUserId,
                    CreatedAt = message.CreatedAt
                })
                .ToList();
        }
    }
}
