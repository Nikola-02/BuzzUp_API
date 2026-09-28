using BuzzUp_API.Application.DTO.Country;
using BuzzUp_API.Application.UseCases;

namespace BuzzUp_API.Application.UseCases.Commands.Country
{
    public interface IUpdateCountryCommand : ICommand<CountryUpdateDTO>
    {
    }
}
