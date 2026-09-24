using BuzzUp_API.Application.DTO.Notifications;
using System.Collections.Generic;

namespace BuzzUp_API.Application.UseCases.Queries.Notifications
{
    public interface IGetNotificationsQuery : IQuery<List<NotificationDTO>, NotificationSearch>
    {
    }
}
