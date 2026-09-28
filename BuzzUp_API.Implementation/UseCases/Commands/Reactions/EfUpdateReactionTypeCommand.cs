using AutoMapper;
using BuzzUp_API.Application.DTO.Reactions;
using BuzzUp_API.Application.UseCases.Commands.Reactions;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Reactions
{
    public class EfUpdateReactionTypeCommand : EfUpdateUseCase<ReactionTypeUpdateDTO, ReactionType>, IUpdateReactionTypeCommand
    {
        public EfUpdateReactionTypeCommand(BuzzUpContext context, IMapper mapper, IValidator<ReactionTypeUpdateDTO> validator)
            : base(context, mapper, validator)
        {
        }

        public override int Id => 43;

        public override string Name => "Update Reaction Type";
    }
}
