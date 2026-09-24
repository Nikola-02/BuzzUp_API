using BuzzUp_API.Application.DTO.Notifications;

namespace BuzzUp_API.Application.UseCases.Commands.Notifications
{
    public interface IMarkAllNotificationsReadCommand : ICommand<NotificationSearch>
    {
    }
}
