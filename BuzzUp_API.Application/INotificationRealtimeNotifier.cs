using BuzzUp_API.Application.DTO.Notifications;

namespace BuzzUp_API.Application
{
    public interface INotificationRealtimeNotifier
    {
        void NotifyNewNotification(int recipientUserId, NotificationDTO notification);
    }
}
