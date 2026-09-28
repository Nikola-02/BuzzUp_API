using AutoMapper;
using BuzzUp_API.Application.DTO.Country;
using BuzzUp_API.Application.UseCases.Commands.Country;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Country
{
    public class EfCreateCountryCommand : EfCreateUseCase<CountryInsertDTO, Domain.Country>, ICreateCountryCommand
    {
        public EfCreateCountryCommand(BuzzUpContext context, IMapper mapper, IValidator<CountryInsertDTO> validator)
            : base(context, mapper, validator)
        {
        }

        public override int Id => 35;

        public override string Name => "Create Country";
    }
}
