using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Friendships;
using BuzzUp_API.Application.UseCases.Commands.Friendships;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.Validators.Friendship;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Friendships
{
    public class EfSendFriendRequestCommand : EfUseCase, ISendFriendRequestCommand
    {
        private readonly FriendshipInsertValidator _validator;
        private readonly IApplicationActor _actor;

        public EfSendFriendRequestCommand(
            BuzzUpContext context,
            FriendshipInsertValidator validator,
            IApplicationActor actor)
            : base(context)
        {
            _validator = validator;
            _actor = actor;
        }

        public int Id => 16;

        public string Name => "Send Friend Request";

        public void Execute(FriendshipInsertDTO data)
        {
            _validator.ValidateAndThrow(data);

            var pendingId = Context.FriendRequestStatuses
                .Where(s => s.Name == "Pending" && s.IsActive && s.DeletedAt == null)
                .Select(s => s.Id)
                .First();

            Context.Friendships.Add(new Friendship
            {
                FriendRequestStatusId = pendingId,
                SenderUserId = _actor.Id,
                ReceiverUserId = data.UserId
            });

            var friendRequestTypeId = Context.NotificationTypes
                .Where(t => t.Name == "FriendRequest" && t.IsActive && t.DeletedAt == null)
                .Select(t => t.Id)
                .First();

            Context.Notifications.Add(new Notification
            {
                RecipientUserId = data.UserId,
                ActorUserId = _actor.Id,
                NotificationTypeId = friendRequestTypeId
            });

            Context.SaveChanges();
        }
    }
}
