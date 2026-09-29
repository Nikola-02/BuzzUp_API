using AutoMapper;
using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.DTO.Country;
using BuzzUp_API.Application.UseCases.Queries.Country;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation;
using System.Linq;

namespace BuzzUp_API.Implementation.UseCases.Queries.Country
{
    public class EfGetCountriesQuery : EfUseCaseMapper, IGetCountriesQuery
    {
        public EfGetCountriesQuery(BuzzUpContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public int Id => 9;

        public string Name => "Search Countries";

        public PagedResponse<CountryDto> Execute(CountrySearch search)
        {
            var query = Context.Countries
                .Where(country => country.IsActive && country.DeletedAt == null)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(country => country.Name.Contains(search.Keyword));
            }

            query = query.OrderBy(country => country.Name).ThenBy(country => country.Id);

            return query.AsPagedReponse<Country, CountryDto>(search, Mapper);
        }
    }
}
