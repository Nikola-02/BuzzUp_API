using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Chats;
using BuzzUp_API.API.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BuzzUp_API.API.Core
{
    public class ChatRealtimeNotifier : IChatRealtimeNotifier
    {
        private readonly IHubContext<ChatHub> _chatHubContext;

        public ChatRealtimeNotifier(IHubContext<ChatHub> chatHubContext)
        {
            _chatHubContext = chatHubContext;
        }

        public void NotifyNewChatMessage(int recipientUserId, ChatRealtimeMessageDTO message)
        {
            _ = _chatHubContext.Clients.Group(ChatHub.UserGroupName(recipientUserId))
                .SendAsync("ReceiveMessage", message);
        }
    }
}
