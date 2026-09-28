namespace BuzzUp_API.Application.DTO.Feelings
{
    public class FeelingTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
