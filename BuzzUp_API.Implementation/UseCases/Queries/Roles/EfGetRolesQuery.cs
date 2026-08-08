using AutoMapper;
using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.UseCases.Queries.Roles;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases.Queries.Roles
{
    public class EfGetRolesQuery : EfSearchLookupUseCase<LookupMiniDTO,Role>, IGetRolesQuery
    {
        public EfGetRolesQuery(BuzzUpContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 7;

        public override string Name => "Search Roles";
    }
}
