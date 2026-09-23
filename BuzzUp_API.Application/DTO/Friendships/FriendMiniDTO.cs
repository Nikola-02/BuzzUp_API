namespace BuzzUp_API.Application.DTO.Friendships
{
    public class FriendMiniDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public bool IsOnline { get; set; }
        public int PostCount { get; set; }
        public int FriendCount { get; set; }
        public DateTime? FriendsSince { get; set; }
    }
}
