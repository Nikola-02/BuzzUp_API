using BuzzUp_API.Application.DTO.Reactions;
using System.Collections.Generic;

namespace BuzzUp_API.Application.UseCases.Queries.Reactions
{
    public interface IGetReactionTypesQuery : IQuery<List<ReactionTypeDTO>, ReactionTypeSearch>
    {
    }
}
