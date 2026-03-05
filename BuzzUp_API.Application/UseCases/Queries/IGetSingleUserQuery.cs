using BuzzUp_API.Application.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Application.UseCases.Queries
{
    public interface IGetSingleUserQuery : IQuery<UserDTO,int>
    {
    }
}
