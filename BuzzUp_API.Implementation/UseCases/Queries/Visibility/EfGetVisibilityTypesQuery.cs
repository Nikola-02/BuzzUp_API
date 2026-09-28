using AutoMapper;
using AutoMapper.QueryableExtensions;
using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.UseCases.Queries.Visibility;
using BuzzUp_API.DataAccess;

namespace BuzzUp_API.Implementation.UseCases.Queries.Visibility
{
    public class EfGetVisibilityTypesQuery : EfUseCaseMapper, IGetVisibilityTypesQuery
    {
        public EfGetVisibilityTypesQuery(BuzzUpContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public int Id => 49;

        public string Name => "Search Visibility Types";

        public List<LookupMiniDTO> Execute(TablesSearch search)
        {
            var query = Context.VisibilityTypes
                .Where(visibilityType => visibilityType.IsActive && visibilityType.DeletedAt == null)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(visibilityType => visibilityType.Name.Contains(search.Keyword));
            }

            return query
                .OrderBy(visibilityType => visibilityType.Id)
                .ProjectTo<LookupMiniDTO>(Mapper.ConfigurationProvider)
                .ToList();
        }
    }
}
