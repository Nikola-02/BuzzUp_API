using BuzzUp_API.Application.DTO.Friendships;
using System.Collections.Generic;

namespace BuzzUp_API.Application.UseCases.Queries.Friendships
{
    public interface IGetIncomingFriendRequestsQuery : IQuery<List<FriendMiniDTO>, FriendSearch>
    {
    }
}
