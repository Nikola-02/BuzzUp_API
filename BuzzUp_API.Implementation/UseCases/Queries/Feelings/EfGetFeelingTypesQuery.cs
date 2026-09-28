using AutoMapper;
using AutoMapper.QueryableExtensions;
using BuzzUp_API.Application.DTO.Feelings;
using BuzzUp_API.Application.UseCases.Queries.Feelings;
using BuzzUp_API.DataAccess;

namespace BuzzUp_API.Implementation.UseCases.Queries.Feelings
{
    public class EfGetFeelingTypesQuery : EfUseCaseMapper, IGetFeelingTypesQuery
    {
        public EfGetFeelingTypesQuery(BuzzUpContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public int Id => 38;

        public string Name => "Search Feeling Types";

        public List<FeelingTypeDTO> Execute(FeelingTypeSearch search)
        {
            var query = Context.FeelingTypes
                .Where(feelingType => feelingType.IsActive && feelingType.DeletedAt == null)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(feelingType => feelingType.Name.Contains(search.Keyword));
            }

            return query
                .OrderBy(feelingType => feelingType.Id)
                .ProjectTo<FeelingTypeDTO>(Mapper.ConfigurationProvider)
                .ToList();
        }
    }
}
