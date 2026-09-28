using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Notifications;
using BuzzUp_API.API.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BuzzUp_API.API.Core
{
    public class NotificationRealtimeNotifier : INotificationRealtimeNotifier
    {
        private readonly IHubContext<ChatHub> _chatHubContext;

        public NotificationRealtimeNotifier(IHubContext<ChatHub> chatHubContext)
        {
            _chatHubContext = chatHubContext;
        }

        public void NotifyNewNotification(int recipientUserId, NotificationDTO notification)
        {
            _ = _chatHubContext.Clients.Group(ChatHub.UserGroupName(recipientUserId))
                .SendAsync("ReceiveNotification", notification);
        }
    }
}
