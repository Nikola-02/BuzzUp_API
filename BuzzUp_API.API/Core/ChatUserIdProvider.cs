using Microsoft.AspNetCore.SignalR;

namespace BuzzUp_API.API.Core
{
    public class ChatUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst("Id")?.Value;
        }
    }
}
