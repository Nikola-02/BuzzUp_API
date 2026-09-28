using AutoMapper;
using BuzzUp_API.Application.DTO.Roles;
using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.UseCases.Commands.Roles;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Roles
{
    public class EfUpdateRoleCommand : EfUpdateUseCase<RoleUpdateDTO, Role>, IUpdateRoleCommand
    {
        public EfUpdateRoleCommand(BuzzUpContext context, IMapper mapper, IValidator<RoleUpdateDTO> validator)
            : base(context, mapper, validator)
        {
        }

        public override int Id => 53;

        public override string Name => "Update Role";

        protected override void EnsureCanUpdate(RoleUpdateDTO request, Role entity)
        {
            if (!IsSystemRole(entity.Name))
            {
                return;
            }

            if (!string.Equals(entity.Name, request.Name, StringComparison.OrdinalIgnoreCase))
            {
                throw new ConflictException("Admin and User roles cannot be renamed.");
            }
        }

        private static bool IsSystemRole(string roleName)
        {
            return string.Equals(roleName, "Admin", StringComparison.OrdinalIgnoreCase)
                || string.Equals(roleName, "User", StringComparison.OrdinalIgnoreCase);
        }
    }
}
