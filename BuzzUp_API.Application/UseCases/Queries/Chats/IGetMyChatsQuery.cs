using BuzzUp_API.Application.DTO.Chats;
using System.Collections.Generic;

namespace BuzzUp_API.Application.UseCases.Queries.Chats
{
    public interface IGetMyChatsQuery : IQuery<List<ChatInboxItemDTO>, ChatSearch>
    {
    }
}
