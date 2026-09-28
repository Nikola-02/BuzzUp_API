using BuzzUp_API.Application.DTO.Roles;
using BuzzUp_API.DataAccess;
using FluentValidation;

namespace BuzzUp_API.Implementation.Validators.Lookup
{
    public class RoleInsertValidator : AbstractValidator<RoleInsertDTO>
    {
        public RoleInsertValidator(BuzzUpContext ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(30).WithMessage("Maximum length for Name is 30.")
                .Must(name => !ctx.Roles.Any(role => role.Name == name && role.IsActive && role.DeletedAt == null))
                .WithMessage("Name already exists.");
        }
    }

    public class RoleUpdateValidator : AbstractValidator<RoleUpdateDTO>
    {
        public RoleUpdateValidator(BuzzUpContext ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(30).WithMessage("Maximum length for Name is 30.")
                .Must((dto, name) => !ctx.Roles.Any(role => role.Name == name && role.Id != dto.Id && role.IsActive && role.DeletedAt == null))
                .WithMessage("Name already exists.");
        }
    }
}
