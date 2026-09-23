using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Friendships;
using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.UseCases.Commands.Friendships;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Implementation.Validators.Friendship;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Friendships
{
    public class EfRejectFriendRequestCommand : EfUseCase, IRejectFriendRequestCommand
    {
        private readonly FriendshipRejectValidator _validator;
        private readonly IApplicationActor _actor;

        public EfRejectFriendRequestCommand(
            BuzzUpContext context,
            FriendshipRejectValidator validator,
            IApplicationActor actor)
            : base(context)
        {
            _validator = validator;
            _actor = actor;
        }

        public int Id => 20;

        public string Name => "Reject Friend Request";

        public void Execute(FriendshipInsertDTO data)
        {
            _validator.ValidateAndThrow(data);

            var friendship = Context.Friendships
                .FirstOrDefault(f =>
                    f.IsActive &&
                    f.DeletedAt == null &&
                    f.FriendRequestStatus.Name == "Pending" &&
                    f.SenderUserId == data.UserId &&
                    f.ReceiverUserId == _actor.Id);

            if (friendship == null)
            {
                throw new ForbiddenException("You cannot reject this friend request.");
            }

            Context.Friendships.Remove(friendship);
            Context.SaveChanges();
        }
    }
}
