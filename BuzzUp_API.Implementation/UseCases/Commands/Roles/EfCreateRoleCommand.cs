using AutoMapper;
using BuzzUp_API.Application.DTO.Roles;
using BuzzUp_API.Application.UseCases.Commands.Roles;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Roles
{
    public class EfCreateRoleCommand : EfCreateUseCase<RoleInsertDTO, Role>, ICreateRoleCommand
    {
        public EfCreateRoleCommand(BuzzUpContext context, IMapper mapper, IValidator<RoleInsertDTO> validator)
            : base(context, mapper, validator)
        {
        }

        public override int Id => 52;

        public override string Name => "Create Role";
    }
}
