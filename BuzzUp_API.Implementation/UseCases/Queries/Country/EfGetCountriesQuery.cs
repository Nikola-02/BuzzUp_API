using AutoMapper;
using AutoMapper.QueryableExtensions;
using BuzzUp_API.Application.DTO.Country;
using BuzzUp_API.Application.UseCases;
using BuzzUp_API.Application.UseCases.Queries.Country;
using BuzzUp_API.Application.UseCases.Queries.Users;
using BuzzUp_API.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases.Queries.Country
{
    public class EfGetCountriesQuery : EfUseCaseMapper, IGetCountriesQuery
    {
        public EfGetCountriesQuery(BuzzUpContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public int Id => 9;

        public string Name => "Search Countries";

        public List<CountryDto> Execute(CountrySearch search)
        {
            var query = Context.Countries.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.Contains(search.Keyword));
            }

            return query
                    .ProjectTo<CountryDto>(Mapper.ConfigurationProvider)
                    .ToList();
        }
    }
}
