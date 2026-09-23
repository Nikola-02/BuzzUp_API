using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Friendships;
using BuzzUp_API.Application.UseCases.Queries.Friendships;
using BuzzUp_API.DataAccess;

namespace BuzzUp_API.Implementation.UseCases.Queries.Friendships
{
    public class EfGetIncomingFriendRequestsQuery : EfUseCase, IGetIncomingFriendRequestsQuery
    {
        private readonly IApplicationActor _actor;

        public EfGetIncomingFriendRequestsQuery(BuzzUpContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 19;

        public string Name => "Get Incoming Friend Requests";

        public List<FriendMiniDTO> Execute(FriendSearch search)
        {
            var actorId = _actor.Id;

            return Context.Friendships
                .Where(f => f.IsActive && f.DeletedAt == null)
                .Where(f => f.FriendRequestStatus.Name == "Pending")
                .Where(f => f.ReceiverUserId == actorId && f.Sender.IsActive && f.Sender.DeletedAt == null)
                .Select(f => new FriendMiniDTO
                {
                    Id = f.Sender.Id,
                    FirstName = f.Sender.FirstName,
                    LastName = f.Sender.LastName,
                    Username = f.Sender.Username,
                    Image = f.Sender.Image,
                    IsOnline = f.Sender.IsOnline
                })
                .OrderByDescending(u => u.IsOnline)
                .ThenBy(u => u.FirstName)
                .ToList();
        }
    }
}
