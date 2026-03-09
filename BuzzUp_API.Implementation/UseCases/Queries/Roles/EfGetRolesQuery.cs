using AutoMapper;
using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.UseCases.Queries.Roles;
using BuzzUp_API.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases.Queries.Roles
{
    public class EfGetRolesQuery : EfUseCaseMapper, IGetRolesQuery
    {
        public EfGetRolesQuery(BuzzUpContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public int Id => 7;

        public string Name => "Search Roles";

        public PagedResponse<LookupMiniDTO> Execute(TablesSearch search)
        {
            throw new NotImplementedException();
        }
    }
}
