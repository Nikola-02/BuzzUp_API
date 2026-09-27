using System.Collections.Generic;

namespace BuzzUp_API.Application.DTO.Comments
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CommentDTO> Replies { get; set; } = new();
    }
}
