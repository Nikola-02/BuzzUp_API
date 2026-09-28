namespace BuzzUp_API.Application.DTO.Chats
{
    public class ChatInboxItemDTO
    {
        public int Id { get; set; }
        public ChatOtherUserDTO OtherUser { get; set; }
        public bool HasUnread { get; set; }
    }
}
