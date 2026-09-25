using BuzzUp_API.DataAccess;

namespace BuzzUp_API.Implementation.UseCases.Notifications
{
    internal static class FriendshipNotificationCleanup
    {
        public static void RemoveBetween(BuzzUpContext context, int userIdA, int userIdB, params string[] typeNames)
        {
            var typeIds = context.NotificationTypes
                .Where(t => typeNames.Contains(t.Name) && t.IsActive && t.DeletedAt == null)
                .Select(t => t.Id)
                .ToList();

            var rows = context.Notifications
                .Where(n => n.IsActive && n.DeletedAt == null)
                .Where(n => typeIds.Contains(n.NotificationTypeId))
                .Where(n =>
                    (n.ActorUserId == userIdA && n.RecipientUserId == userIdB) ||
                    (n.ActorUserId == userIdB && n.RecipientUserId == userIdA))
                .ToList();

            context.Notifications.RemoveRange(rows);
        }
    }
}
