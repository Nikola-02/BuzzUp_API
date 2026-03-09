using AutoMapper;
using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.UseCases.Queries.Users;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases.Queries.Users
{
    public class EfGetSingleUserQuery : EfFindUseCase<UserDTO, User>, IGetSingleUserQuery
    {
        public EfGetSingleUserQuery(BuzzUpContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 4;

        public override string Name => "Find User";
    }
}
