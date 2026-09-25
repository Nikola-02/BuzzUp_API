using BuzzUp_API.Application.DTO.Reactions;

namespace BuzzUp_API.Application.UseCases.Commands.Reactions
{
    public interface IReactToPostCommand : ICommand<ReactionInsertDTO>
    {
    }
}
