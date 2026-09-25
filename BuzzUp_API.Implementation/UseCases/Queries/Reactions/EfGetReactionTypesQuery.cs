using AutoMapper;
using AutoMapper.QueryableExtensions;
using BuzzUp_API.Application.DTO.Reactions;
using BuzzUp_API.Application.UseCases.Queries.Reactions;
using BuzzUp_API.DataAccess;

namespace BuzzUp_API.Implementation.UseCases.Queries.Reactions
{
    public class EfGetReactionTypesQuery : EfUseCaseMapper, IGetReactionTypesQuery
    {
        public EfGetReactionTypesQuery(BuzzUpContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public int Id => 25;

        public string Name => "Search Reaction Types";

        public List<ReactionTypeDTO> Execute(ReactionTypeSearch search)
        {
            var query = Context.ReactionTypes
                .Where(x => x.IsActive && x.DeletedAt == null)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.Contains(search.Keyword));
            }

            return query
                .OrderBy(x => x.Id)
                .ProjectTo<ReactionTypeDTO>(Mapper.ConfigurationProvider)
                .ToList();
        }
    }
}
