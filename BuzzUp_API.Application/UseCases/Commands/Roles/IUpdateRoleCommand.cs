using BuzzUp_API.Application.DTO.Roles;
using BuzzUp_API.Application.UseCases;

namespace BuzzUp_API.Application.UseCases.Commands.Roles
{
    public interface IUpdateRoleCommand : ICommand<RoleUpdateDTO>
    {
    }
}
