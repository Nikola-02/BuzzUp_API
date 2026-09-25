using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Friendships;
using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.UseCases.Commands.Friendships;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Implementation.UseCases.Notifications;
using BuzzUp_API.Implementation.Validators.Friendship;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Friendships
{
    public class EfUnfriendCommand : EfUseCase, IUnfriendCommand
    {
        private readonly FriendshipUnfriendValidator _validator;
        private readonly IApplicationActor _actor;

        public EfUnfriendCommand(
            BuzzUpContext context,
            FriendshipUnfriendValidator validator,
            IApplicationActor actor)
            : base(context)
        {
            _validator = validator;
            _actor = actor;
        }

        public int Id => 21;

        public string Name => "Unfriend User";

        public void Execute(FriendshipInsertDTO data)
        {
            _validator.ValidateAndThrow(data);

            var friendship = Context.Friendships
                .FirstOrDefault(f =>
                    f.IsActive &&
                    f.DeletedAt == null &&
                    f.FriendRequestStatus.Name == "Accepted" &&
                    ((f.SenderUserId == _actor.Id && f.ReceiverUserId == data.UserId) ||
                     (f.ReceiverUserId == _actor.Id && f.SenderUserId == data.UserId)));

            if (friendship == null)
            {
                throw new ForbiddenException("You cannot unfriend this user.");
            }

            FriendshipNotificationCleanup.RemoveBetween(
                Context,
                friendship.SenderUserId,
                friendship.ReceiverUserId,
                "FriendRequest",
                "FriendAccepted");

            Context.Friendships.Remove(friendship);
            Context.SaveChanges();
        }
    }
}
