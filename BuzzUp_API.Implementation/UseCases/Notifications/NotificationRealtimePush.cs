using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Notifications;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;

namespace BuzzUp_API.Implementation.UseCases.Notifications
{
    internal static class NotificationRealtimePush
    {
        public static void NotifyNew(BuzzUpContext context, INotificationRealtimeNotifier notifier, Notification notification)
        {
            if (notification == null || notifier == null)
            {
                return;
            }

            var actor = context.Users.First(user => user.Id == notification.ActorUserId);
            var typeName = context.NotificationTypes
                .Where(notificationType => notificationType.Id == notification.NotificationTypeId)
                .Select(notificationType => notificationType.Name)
                .First();

            string reactionTypeName = null;
            string reactionTypeIcon = null;
            if (notification.ReactionTypeId.HasValue)
            {
                var reactionType = context.ReactionTypes.FirstOrDefault(existingReactionType =>
                    existingReactionType.Id == notification.ReactionTypeId.Value);
                reactionTypeName = reactionType?.Name;
                reactionTypeIcon = reactionType?.Icon;
            }

            notifier.NotifyNewNotification(notification.RecipientUserId, new NotificationDTO
            {
                Id = notification.Id,
                ActorId = actor.Id,
                ActorFirstName = actor.FirstName,
                ActorLastName = actor.LastName,
                ActorUsername = actor.Username,
                ActorImage = actor.Image,
                Type = typeName,
                PostId = notification.PostId,
                ReactionTypeName = reactionTypeName,
                ReactionTypeIcon = reactionTypeIcon,
                IsRead = notification.IsRead,
                CreatedAt = notification.CreatedAt
            });
        }
    }
}
