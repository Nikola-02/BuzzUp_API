using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Chats;
using BuzzUp_API.Application.UseCases.Queries.Chats;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.Validators.Chat;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Queries.Chats
{
    public class EfOpenDirectChatQuery : EfUseCase, IOpenDirectChatQuery
    {
        private readonly ChatOpenValidator _validator;
        private readonly IApplicationActor _actor;

        public EfOpenDirectChatQuery(
            BuzzUpContext context,
            ChatOpenValidator validator,
            IApplicationActor actor)
            : base(context)
        {
            _validator = validator;
            _actor = actor;
        }

        public int Id => 31;

        public string Name => "Open Direct Chat";

        public ChatDTO Execute(ChatOpenDTO request)
        {
            _validator.ValidateAndThrow(request);

            var actorUserId = _actor.Id;
            var otherUserId = request.UserId;

            var existingChatId = Context.UserChats
                .Where(actorMembership =>
                    actorMembership.UserId == actorUserId &&
                    actorMembership.IsActive &&
                    actorMembership.DeletedAt == null &&
                    actorMembership.Chat.IsActive &&
                    actorMembership.Chat.DeletedAt == null)
                .Where(actorMembership =>
                    Context.UserChats.Any(otherMembership =>
                        otherMembership.ChatId == actorMembership.ChatId &&
                        otherMembership.UserId == otherUserId &&
                        otherMembership.IsActive &&
                        otherMembership.DeletedAt == null))
                .Select(actorMembership => actorMembership.ChatId)
                .FirstOrDefault();

            if (existingChatId == 0)
            {
                var newChat = new Chat();
                Context.Chats.Add(newChat);
                Context.UserChats.Add(new UserChat { UserId = actorUserId, Chat = newChat });
                Context.UserChats.Add(new UserChat { UserId = otherUserId, Chat = newChat });
                Context.SaveChanges();
                existingChatId = newChat.Id;
            }

            var otherUser = Context.Users
                .Where(user => user.Id == otherUserId && user.IsActive && user.DeletedAt == null)
                .Select(user => new ChatOtherUserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Image = user.Image
                })
                .First();

            return new ChatDTO
            {
                Id = existingChatId,
                OtherUser = otherUser
            };
        }
    }
}
