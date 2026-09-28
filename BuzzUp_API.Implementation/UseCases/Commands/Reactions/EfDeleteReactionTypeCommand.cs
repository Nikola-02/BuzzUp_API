using BuzzUp_API.Application.UseCases.Commands.Reactions;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases;

namespace BuzzUp_API.Implementation.UseCases.Commands.Reactions
{
    public class EfDeleteReactionTypeCommand : EfDeleteUseCase<ReactionType>, IDeleteReactionTypeCommand
    {
        public EfDeleteReactionTypeCommand(BuzzUpContext context) : base(context)
        {
        }

        public override int Id => 44;

        public override string Name => "Delete Reaction Type";
    }
}
