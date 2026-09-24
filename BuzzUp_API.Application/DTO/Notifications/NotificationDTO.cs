namespace BuzzUp_API.Application.DTO.Notifications
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public int ActorId { get; set; }
        public string ActorFirstName { get; set; }
        public string ActorLastName { get; set; }
        public string ActorUsername { get; set; }
        public string ActorImage { get; set; }
        public string Type { get; set; }
        public int? PostId { get; set; }
        public string ReactionTypeName { get; set; }
        public string ReactionTypeIcon { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
