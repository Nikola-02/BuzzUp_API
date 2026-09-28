using System.Collections.Concurrent;
using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BuzzUp_API.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private static readonly ConcurrentDictionary<int, int> OnlineConnectionCounts = new();

        private readonly BuzzUpContext _context;
        private readonly IHubContext<ChatHub> _chatHubContext;

        public ChatHub(BuzzUpContext context, IHubContext<ChatHub> chatHubContext)
        {
            _context = context;
            _chatHubContext = chatHubContext;
        }

        public static string UserGroupName(int userId) => $"user-{userId}";

        public override async Task OnConnectedAsync()
        {
            var connectedUserId = GetConnectedUserId();
            if (connectedUserId.HasValue)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, UserGroupName(connectedUserId.Value));
                var onlineConnectionCount = OnlineConnectionCounts.AddOrUpdate(connectedUserId.Value, 1, (_, count) => count + 1);
                if (onlineConnectionCount == 1)
                {
                    await SetUserOnlineAndNotifyFriends(connectedUserId.Value, true);
                }
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectedUserId = GetConnectedUserId();
            if (connectedUserId.HasValue)
            {
                var onlineConnectionCount = OnlineConnectionCounts.AddOrUpdate(
                    connectedUserId.Value,
                    0,
                    (_, count) => Math.Max(0, count - 1));

                if (onlineConnectionCount == 0)
                {
                    OnlineConnectionCounts.TryRemove(connectedUserId.Value, out _);
                    await SetUserOnlineAndNotifyFriends(connectedUserId.Value, false);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        private int? GetConnectedUserId()
        {
            var userIdClaim = Context.User?.FindFirst("Id")?.Value;
            if (!int.TryParse(userIdClaim, out var connectedUserId))
            {
                return null;
            }

            return connectedUserId;
        }

        private async Task SetUserOnlineAndNotifyFriends(int connectedUserId, bool isOnline)
        {
            var user = _context.Users.FirstOrDefault(existingUser =>
                existingUser.Id == connectedUserId &&
                existingUser.IsActive &&
                existingUser.DeletedAt == null);

            if (user == null)
            {
                return;
            }

            user.IsOnline = isOnline;
            _context.SaveChanges();

            var friendUserIds = _context.Friendships
                .Where(friendship =>
                    friendship.IsActive &&
                    friendship.DeletedAt == null &&
                    friendship.FriendRequestStatus.Name == "Accepted" &&
                    (friendship.SenderUserId == connectedUserId || friendship.ReceiverUserId == connectedUserId))
                .Select(friendship => friendship.SenderUserId == connectedUserId
                    ? friendship.ReceiverUserId
                    : friendship.SenderUserId)
                .Distinct()
                .ToList();

            var presence = new UserPresenceDTO
            {
                UserId = connectedUserId,
                IsOnline = isOnline
            };

            foreach (var friendUserId in friendUserIds)
            {
                await _chatHubContext.Clients.Group(UserGroupName(friendUserId)).SendAsync("PresenceChanged", presence);
            }
        }
    }
}
