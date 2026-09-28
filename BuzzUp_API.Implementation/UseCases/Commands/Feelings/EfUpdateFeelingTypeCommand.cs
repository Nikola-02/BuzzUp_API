using AutoMapper;
using BuzzUp_API.Application.DTO.Feelings;
using BuzzUp_API.Application.UseCases.Commands.Feelings;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Feelings
{
    public class EfUpdateFeelingTypeCommand : EfUpdateUseCase<FeelingTypeUpdateDTO, FeelingType>, IUpdateFeelingTypeCommand
    {
        public EfUpdateFeelingTypeCommand(BuzzUpContext context, IMapper mapper, IValidator<FeelingTypeUpdateDTO> validator)
            : base(context, mapper, validator)
        {
        }

        public override int Id => 40;

        public override string Name => "Update Feeling Type";
    }
}
