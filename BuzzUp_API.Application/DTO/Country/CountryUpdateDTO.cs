using BuzzUp_API.Application.DTO;

namespace BuzzUp_API.Application.DTO.Country
{
    public class CountryUpdateDTO : IUpdateDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
