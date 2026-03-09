using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Application.UseCases.Queries.Users
{
    public interface IGetUsersQuery : IQuery<PagedResponse<UserMiniDTO>, UserSearch>
    {
    }
}
