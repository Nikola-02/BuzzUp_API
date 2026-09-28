using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Friendships;
using BuzzUp_API.Application.UseCases.Commands.Friendships;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases.Notifications;
using BuzzUp_API.Implementation.Validators.Friendship;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Friendships
{
    public class EfSendFriendRequestCommand : EfUseCase, ISendFriendRequestCommand
    {
        private readonly FriendshipInsertValidator _validator;
        private readonly IApplicationActor _actor;
        private readonly INotificationRealtimeNotifier _notificationRealtimeNotifier;

        public EfSendFriendRequestCommand(
            BuzzUpContext context,
            FriendshipInsertValidator validator,
            IApplicationActor actor,
            INotificationRealtimeNotifier notificationRealtimeNotifier)
            : base(context)
        {
            _validator = validator;
            _actor = actor;
            _notificationRealtimeNotifier = notificationRealtimeNotifier;
        }

        public int Id => 16;

        public string Name => "Send Friend Request";

        public void Execute(FriendshipInsertDTO data)
        {
            _validator.ValidateAndThrow(data);

            var pendingId = Context.FriendRequestStatuses
                .Where(s => s.Name == "Pending" && s.IsActive && s.DeletedAt == null)
                .Select(s => s.Id)
                .First();

            Context.Friendships.Add(new Friendship
            {
                FriendRequestStatusId = pendingId,
                SenderUserId = _actor.Id,
                ReceiverUserId = data.UserId
            });

            var friendRequestTypeId = Context.NotificationTypes
                .Where(t => t.Name == "FriendRequest" && t.IsActive && t.DeletedAt == null)
                .Select(t => t.Id)
                .First();

            var friendRequestNotification = new Notification
            {
                RecipientUserId = data.UserId,
                ActorUserId = _actor.Id,
                NotificationTypeId = friendRequestTypeId
            };
            Context.Notifications.Add(friendRequestNotification);

            Context.SaveChanges();
            NotificationRealtimePush.NotifyNew(Context, _notificationRealtimeNotifier, friendRequestNotification);
        }
    }
}
