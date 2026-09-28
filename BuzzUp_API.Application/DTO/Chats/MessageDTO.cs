namespace BuzzUp_API.Application.DTO.Chats
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int SenderId { get; set; }
        public bool IsMine { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
