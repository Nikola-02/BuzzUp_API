using BuzzUp_API.Application.DTO.Chats;
using System.Collections.Generic;

namespace BuzzUp_API.Application.UseCases.Queries.Chats
{
    public interface IGetChatMessagesQuery : IQuery<List<MessageDTO>, int>
    {
    }
}
