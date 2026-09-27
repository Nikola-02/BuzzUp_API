namespace BuzzUp_API.Application.DTO.Comments
{
    public class CommentInsertDTO
    {
        public int PostId { get; set; }
        public int? ParentId { get; set; }
        public string Content { get; set; }
    }
}
