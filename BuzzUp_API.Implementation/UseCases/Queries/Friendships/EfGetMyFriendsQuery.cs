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
            var actorId = _actor.Id;

            var friends = Context.Friendships
                .Where(f => f.IsActive && f.DeletedAt == null)
                .Where(f => f.FriendRequestStatus.Name == "Accepted")
                .Where(f =>
                    (f.SenderUserId == actorId && f.Receiver.IsActive && f.Receiver.DeletedAt == null) ||
                    (f.ReceiverUserId == actorId && f.Sender.IsActive && f.Sender.DeletedAt == null))
                .Select(f => new FriendMiniDTO
                {
                    Id = f.SenderUserId == actorId ? f.Receiver.Id : f.Sender.Id,
                    FirstName = f.SenderUserId == actorId ? f.Receiver.FirstName : f.Sender.FirstName,
                    LastName = f.SenderUserId == actorId ? f.Receiver.LastName : f.Sender.LastName,
                    Username = f.SenderUserId == actorId ? f.Receiver.Username : f.Sender.Username,
                    Image = f.SenderUserId == actorId ? f.Receiver.Image : f.Sender.Image,
                    IsOnline = f.SenderUserId == actorId ? f.Receiver.IsOnline : f.Sender.IsOnline
                });

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
    }
}
