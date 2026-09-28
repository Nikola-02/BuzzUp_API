using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.UseCases.Commands.Roles;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases;

namespace BuzzUp_API.Implementation.UseCases.Commands.Roles
{
    public class EfDeleteRoleCommand : EfDeleteUseCase<Role>, IDeleteRoleCommand
    {
        public EfDeleteRoleCommand(BuzzUpContext context) : base(context)
        {
        }

        public override int Id => 54;

        public override string Name => "Delete Role";

        protected override void EnsureCanDelete(Role entity)
        {
            if (string.Equals(entity.Name, "Admin", StringComparison.OrdinalIgnoreCase)
                || string.Equals(entity.Name, "User", StringComparison.OrdinalIgnoreCase))
            {
                throw new ConflictException("Admin and User roles cannot be deleted.");
            }

            bool roleIsAssignedToUser = Context.Users.Any(user => user.RoleId == entity.Id && user.DeletedAt == null);
            if (roleIsAssignedToUser)
            {
                throw new ConflictException("Role cannot be deleted while users are assigned to it.");
            }
        }
    }
}
