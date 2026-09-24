using BuzzUp_API.Application;
using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.UseCases.Commands.Notifications;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;

namespace BuzzUp_API.Implementation.UseCases.Commands.Notifications
{
    public class EfMarkNotificationReadCommand : EfUseCase, IMarkNotificationReadCommand
    {
        private readonly IApplicationActor _actor;

        public EfMarkNotificationReadCommand(BuzzUpContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 24;

        public string Name => "Mark Notification Read";

        public void Execute(int id)
        {
            var notification = Context.Notifications
                .FirstOrDefault(n => n.Id == id && n.IsActive && n.DeletedAt == null);

            if (notification == null)
            {
                throw new EntityNotFoundException(nameof(Notification), id);
            }

            if (notification.RecipientUserId != _actor.Id)
            {
                throw new ForbiddenException("You cannot mark this notification as read.");
            }

            if (notification.IsRead)
            {
                return;
            }

            notification.IsRead = true;
            Context.SaveChanges();
        }
    }
}
