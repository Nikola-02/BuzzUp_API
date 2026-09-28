using BuzzUp_API.Application.DTO.Chats;

namespace BuzzUp_API.Application
{
    public interface IChatRealtimeNotifier
    {
        void NotifyNewChatMessage(int recipientUserId, ChatRealtimeMessageDTO message);
    }
}
