using AutoMapper;
using BuzzUp_API.Application.DTO.Country;
using BuzzUp_API.Application.UseCases.Commands.Country;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Country
{
    public class EfUpdateCountryCommand : EfUpdateUseCase<CountryUpdateDTO, Domain.Country>, IUpdateCountryCommand
    {
        public EfUpdateCountryCommand(BuzzUpContext context, IMapper mapper, IValidator<CountryUpdateDTO> validator)
            : base(context, mapper, validator)
        {
        }

        public override int Id => 36;

        public override string Name => "Update Country";
    }
}
