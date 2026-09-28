using BuzzUp_API.Application.DTO;

namespace BuzzUp_API.Application.DTO.Roles
{
    public class RoleUpdateDTO : IUpdateDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
