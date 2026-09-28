using AutoMapper;
using BuzzUp_API.Application.DTO.Reactions;
using BuzzUp_API.Application.UseCases.Commands.Reactions;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Reactions
{
    public class EfCreateReactionTypeCommand : EfCreateUseCase<ReactionTypeInsertDTO, ReactionType>, ICreateReactionTypeCommand
    {
        public EfCreateReactionTypeCommand(BuzzUpContext context, IMapper mapper, IValidator<ReactionTypeInsertDTO> validator)
            : base(context, mapper, validator)
        {
        }

        public override int Id => 42;

        public override string Name => "Create Reaction Type";
    }
}
