using BuzzUp_API.Application.DTO.Friendships;

namespace BuzzUp_API.Application.UseCases.Commands.Friendships
{
    public interface IRejectFriendRequestCommand : ICommand<FriendshipInsertDTO>
    {
    }
}
