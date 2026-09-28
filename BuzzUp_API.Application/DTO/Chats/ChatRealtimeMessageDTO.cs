namespace BuzzUp_API.Application.DTO.Chats
{
    public class ChatRealtimeMessageDTO
    {
        public int ChatId { get; set; }
        public int Id { get; set; }
        public string Content { get; set; }
        public int SenderId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
