using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Friendships;
using BuzzUp_API.Application.UseCases.Queries.Friendships;
using BuzzUp_API.DataAccess;

namespace BuzzUp_API.Implementation.UseCases.Queries.Friendships
{
    public class EfGetMyFriendsQuery : EfUseCase, IGetMyFriendsQuery
    {
        private readonly IApplicationActor _actor;

        public EfGetMyFriendsQuery(BuzzUpContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 17;

        public string Name => "Get My Friends";

        public List<FriendMiniDTO> Execute(FriendSearch search)
        {
            var ownerId = search.UserId.HasValue && search.UserId.Value > 0
                ? search.UserId.Value
                : _actor.Id;

            if (ownerId != _actor.Id && !CanSeeFriendsList(ownerId))
            {
                return new List<FriendMiniDTO>();
            }

            var friends = Context.Friendships
                .Where(f => f.IsActive && f.DeletedAt == null)
                .Where(f => f.FriendRequestStatus.Name == "Accepted")
                .Where(f =>
                    (f.SenderUserId == ownerId && f.Receiver.IsActive && f.Receiver.DeletedAt == null) ||
                    (f.ReceiverUserId == ownerId && f.Sender.IsActive && f.Sender.DeletedAt == null))
                .Select(f => new FriendMiniDTO
                {
                    Id = f.SenderUserId == ownerId ? f.Receiver.Id : f.Sender.Id,
                    FirstName = f.SenderUserId == ownerId ? f.Receiver.FirstName : f.Sender.FirstName,
                    LastName = f.SenderUserId == ownerId ? f.Receiver.LastName : f.Sender.LastName,
                    Username = f.SenderUserId == ownerId ? f.Receiver.Username : f.Sender.Username,
                    Image = f.SenderUserId == ownerId ? f.Receiver.Image : f.Sender.Image,
                    IsOnline = f.SenderUserId == ownerId ? f.Receiver.IsOnline : f.Sender.IsOnline,
                    PostCount = Context.Posts.Count(p =>
                        p.IsActive &&
                        p.DeletedAt == null &&
                        p.UserId == (f.SenderUserId == ownerId ? f.Receiver.Id : f.Sender.Id)),
                    FriendCount = Context.Friendships.Count(x =>
                        x.IsActive &&
                        x.DeletedAt == null &&
                        x.FriendRequestStatus.Name == "Accepted" &&
                        (x.SenderUserId == (f.SenderUserId == ownerId ? f.Receiver.Id : f.Sender.Id) ||
                         x.ReceiverUserId == (f.SenderUserId == ownerId ? f.Receiver.Id : f.Sender.Id))),
                    FriendsSince = ownerId == _actor.Id
                        ? (f.UpdatedAt != null ? f.UpdatedAt.Value : f.CreatedAt)
                        : null
                })
                .Where(u => u.Id != _actor.Id);

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                var keyword = search.Keyword.ToLower();
                friends = friends.Where(u =>
                    u.Username.ToLower().Contains(keyword) ||
                    u.FirstName.ToLower().Contains(keyword) ||
                    u.LastName.ToLower().Contains(keyword));
            }

            return friends
                .OrderByDescending(u => u.IsOnline)
                .ThenBy(u => u.FirstName)
                .ToList();
        }

        private bool CanSeeFriendsList(int ownerId)
        {
            var ownerIsPrivate = Context.Users.Any(u =>
                u.Id == ownerId &&
                u.IsActive &&
                u.DeletedAt == null &&
                u.IsPrivate);

            if (!ownerIsPrivate)
            {
                return true;
            }

            return Context.Friendships.Any(f =>
                f.IsActive &&
                f.DeletedAt == null &&
                f.FriendRequestStatus.Name == "Accepted" &&
                ((f.SenderUserId == ownerId && f.ReceiverUserId == _actor.Id) ||
                 (f.ReceiverUserId == ownerId && f.SenderUserId == _actor.Id)));
        }
    }
}
