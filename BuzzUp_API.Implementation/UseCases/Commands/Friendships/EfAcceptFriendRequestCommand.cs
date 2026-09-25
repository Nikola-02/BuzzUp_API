using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Friendships;
using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.UseCases.Commands.Friendships;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases.Notifications;
using BuzzUp_API.Implementation.Validators.Friendship;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Friendships
{
    public class EfAcceptFriendRequestCommand : EfUseCase, IAcceptFriendRequestCommand
    {
        private readonly FriendshipAcceptValidator _validator;
        private readonly IApplicationActor _actor;

        public EfAcceptFriendRequestCommand(
            BuzzUpContext context,
            FriendshipAcceptValidator validator,
            IApplicationActor actor)
            : base(context)
        {
            _validator = validator;
            _actor = actor;
        }

        public int Id => 18;

        public string Name => "Accept Friend Request";

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
                throw new ForbiddenException("You cannot accept this friend request.");
            }

            var acceptedId = Context.FriendRequestStatuses
                .Where(s => s.Name == "Accepted" && s.IsActive && s.DeletedAt == null)
                .Select(s => s.Id)
                .First();

            friendship.FriendRequestStatusId = acceptedId;

            FriendshipNotificationCleanup.RemoveBetween(
                Context,
                friendship.SenderUserId,
                friendship.ReceiverUserId,
                "FriendRequest");

            var friendAcceptedTypeId = Context.NotificationTypes
                .Where(t => t.Name == "FriendAccepted" && t.IsActive && t.DeletedAt == null)
                .Select(t => t.Id)
                .First();

            Context.Notifications.Add(new Notification
            {
                RecipientUserId = friendship.SenderUserId,
                ActorUserId = _actor.Id,
                NotificationTypeId = friendAcceptedTypeId
            });

            Context.SaveChanges();
        }
    }
}
