using BuzzUp_API.Application.DTO.Country;
using BuzzUp_API.DataAccess;
using FluentValidation;

namespace BuzzUp_API.Implementation.Validators.Lookup
{
    public class CountryInsertValidator : AbstractValidator<CountryInsertDTO>
    {
        public CountryInsertValidator(BuzzUpContext ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(30).WithMessage("Maximum length for Name is 30.")
                .Must(name => !ctx.Countries.Any(c => c.Name == name && c.IsActive && c.DeletedAt == null))
                .WithMessage("Name already exists.");
        }
    }

    public class CountryUpdateValidator : AbstractValidator<CountryUpdateDTO>
    {
        public CountryUpdateValidator(BuzzUpContext ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(30).WithMessage("Maximum length for Name is 30.")
                .Must((dto, name) => !ctx.Countries.Any(c => c.Name == name && c.Id != dto.Id && c.IsActive && c.DeletedAt == null))
                .WithMessage("Name already exists.");
        }
    }
}
