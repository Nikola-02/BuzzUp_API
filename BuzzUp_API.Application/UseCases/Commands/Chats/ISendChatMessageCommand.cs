using BuzzUp_API.Application.DTO.Chats;
using BuzzUp_API.Application.UseCases;

namespace BuzzUp_API.Application.UseCases.Commands.Chats
{
    public interface ISendChatMessageCommand : ICommand<MessageInsertDTO>
    {
    }
}
