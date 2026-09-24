using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Notifications;
using BuzzUp_API.Application.UseCases.Commands.Notifications;
using BuzzUp_API.DataAccess;

namespace BuzzUp_API.Implementation.UseCases.Commands.Notifications
{
    public class EfMarkAllNotificationsReadCommand : EfUseCase, IMarkAllNotificationsReadCommand
    {
        private readonly IApplicationActor _actor;

        public EfMarkAllNotificationsReadCommand(BuzzUpContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 23;

        public string Name => "Mark All Notifications Read";

        public void Execute(NotificationSearch data)
        {
            var actorId = _actor.Id;

            var unread = Context.Notifications
                .Where(n => n.IsActive && n.DeletedAt == null)
                .Where(n => n.RecipientUserId == actorId && !n.IsRead)
                .ToList();

            foreach (var notification in unread)
            {
                notification.IsRead = true;
            }

            if (unread.Count > 0)
            {
                Context.SaveChanges();
            }
        }
    }
}
