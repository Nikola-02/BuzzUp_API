using AutoMapper;
using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.Application.UseCases.Queries.Users;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;

namespace BuzzUp_API.Implementation.UseCases.Queries.Users
{
    public class EfGetSingleUserQuery : EfFindUseCase<UserMiniDTO, User>, IGetSingleUserQuery
    {
        private readonly IApplicationActor _actor;

        public EfGetSingleUserQuery(BuzzUpContext context, IMapper mapper, IApplicationActor actor) : base(context, mapper)
        {
            _actor = actor;
        }

        public override int Id => 4;

        public override string Name => "Find User";

        public override UserMiniDTO Execute(int id)
        {
            var dto = base.Execute(id);

            dto.PostCount = Context.Posts.Count(p =>
                p.UserId == id && p.IsActive && p.DeletedAt == null);

            dto.FriendCount = Context.Friendships.Count(f =>
                f.IsActive &&
                f.DeletedAt == null &&
                f.FriendRequestStatus.Name == "Accepted" &&
                (f.SenderUserId == id || f.ReceiverUserId == id));

            dto.FriendshipStatus = ResolveFriendshipStatus(id);

            return dto;
        }

        private string ResolveFriendshipStatus(int userId)
        {
            if (_actor.Id == 0 || _actor.Id == userId)
            {
                return "None";
            }

            var friendship = Context.Friendships
                .Where(f => f.IsActive && f.DeletedAt == null)
                .Where(f =>
                    (f.SenderUserId == _actor.Id && f.ReceiverUserId == userId) ||
                    (f.SenderUserId == userId && f.ReceiverUserId == _actor.Id))
                .OrderByDescending(f => f.CreatedAt)
                .Select(f => new
                {
                    Status = f.FriendRequestStatus.Name,
                    f.SenderUserId
                })
                .FirstOrDefault();

            if (friendship == null)
            {
                return "None";
            }

            if (friendship.Status == "Pending")
            {
                return friendship.SenderUserId == _actor.Id ? "PendingOutgoing" : "PendingIncoming";
            }

            if (friendship.Status == "Accepted")
            {
                return "Accepted";
            }

            return "None";
        }
    }
}
