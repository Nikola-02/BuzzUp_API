using BuzzUp_API.Application.DTO;

namespace BuzzUp_API.Application.DTO.Reactions
{
    public class ReactionTypeUpdateDTO : IUpdateDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
