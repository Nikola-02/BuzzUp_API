using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Friendships;
using BuzzUp_API.Application.UseCases.Commands.Friendships;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Friendships
{
    public class EfSendFriendRequestCommand : EfUseCase, ISendFriendRequestCommand
    {
        private readonly IValidator<FriendshipInsertDTO> _validator;
        private readonly IApplicationActor _actor;

        public EfSendFriendRequestCommand(
            BuzzUpContext context,
            IValidator<FriendshipInsertDTO> validator,
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

            Context.SaveChanges();
        }
    }
}
