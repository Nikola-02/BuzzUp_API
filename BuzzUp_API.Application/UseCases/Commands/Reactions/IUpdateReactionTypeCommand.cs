using BuzzUp_API.Application.DTO.Reactions;
using BuzzUp_API.Application.UseCases;

namespace BuzzUp_API.Application.UseCases.Commands.Reactions
{
    public interface IUpdateReactionTypeCommand : ICommand<ReactionTypeUpdateDTO>
    {
    }
}
