using AutoMapper;
using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Chats;
using BuzzUp_API.Application.UseCases.Commands.Chats;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Chats
{
    public class EfSendChatMessageCommand : EfCreateUseCase<MessageInsertDTO, Message>, ISendChatMessageCommand
    {
        private readonly IApplicationActor _actor;
        private readonly IChatRealtimeNotifier _chatRealtimeNotifier;

        public EfSendChatMessageCommand(
            BuzzUpContext context,
            IMapper mapper,
            IValidator<MessageInsertDTO> validator,
            IApplicationActor actor,
            IChatRealtimeNotifier chatRealtimeNotifier)
            : base(context, mapper, validator)
        {
            _actor = actor;
            _chatRealtimeNotifier = chatRealtimeNotifier;
        }

        public override int Id => 34;

        public override string Name => "Send Chat Message";

        protected override void AfterMap(MessageInsertDTO request, Message entity)
        {
            entity.SenderId = _actor.Id;
            entity.IsRead = false;
        }

        protected override void AfterSave(MessageInsertDTO request, Message savedMessage)
        {
            var senderUserId = _actor.Id;
            var recipientUserId = Context.UserChats
                .Where(membership =>
                    membership.ChatId == savedMessage.ChatId &&
                    membership.UserId != senderUserId &&
                    membership.IsActive &&
                    membership.DeletedAt == null)
                .Select(membership => membership.UserId)
                .FirstOrDefault();

            if (recipientUserId == 0)
            {
                return;
            }

            _chatRealtimeNotifier.NotifyNewChatMessage(recipientUserId, new ChatRealtimeMessageDTO
            {
                ChatId = savedMessage.ChatId,
                Id = savedMessage.Id,
                Content = savedMessage.Content,
                SenderId = savedMessage.SenderId,
                CreatedAt = savedMessage.CreatedAt
            });
        }
    }
}
