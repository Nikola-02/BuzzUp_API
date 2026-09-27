using BuzzUp_API.Application.DTO;

namespace BuzzUp_API.Application.DTO.Comments
{
    public class CommentUpdateDTO : IUpdateDTO
    {
        public int? Id { get; set; }
        public string Content { get; set; }
    }
}
