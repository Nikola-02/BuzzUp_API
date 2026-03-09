using AutoMapper;
using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.Application.UseCases.Queries.Users;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BuzzUp_API.Implementation.UseCases.Queries.Users
{
    public class EfGetUsersQuery : EfUseCaseMapper, IGetUsersQuery
    {
        public EfGetUsersQuery(BuzzUpContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public int Id => 6;

        public string Name => "Search Users";

        public PagedResponse<UserMiniDTO> Execute(UserSearch search)
        {
            var query = Context.Users
                                    .Where(x => x.IsActive && x.DeletedAt == null)
                                    .AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Username.Contains(search.Keyword, StringComparison.CurrentCultureIgnoreCase) ||
                                         x.FirstName.Contains(search.Keyword, StringComparison.CurrentCultureIgnoreCase) ||
                                         x.Username.Contains(search.Keyword, StringComparison.CurrentCultureIgnoreCase));
            }

            return query.AsPagedReponse<User, UserMiniDTO>(search, Mapper);
        }
    }
}
