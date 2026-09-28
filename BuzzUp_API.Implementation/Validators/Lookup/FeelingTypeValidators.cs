using BuzzUp_API.Application.DTO.Feelings;
using BuzzUp_API.DataAccess;
using FluentValidation;

namespace BuzzUp_API.Implementation.Validators.Lookup
{
    public class FeelingTypeInsertValidator : AbstractValidator<FeelingTypeInsertDTO>
    {
        public FeelingTypeInsertValidator(BuzzUpContext ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(30).WithMessage("Maximum length for Name is 30.")
                .Must(name => !ctx.FeelingTypes.Any(f => f.Name == name && f.IsActive && f.DeletedAt == null))
                .WithMessage("Name already exists.");
            RuleFor(x => x.Icon)
                .NotEmpty().WithMessage("Icon is required.")
                .MaximumLength(40).WithMessage("Maximum length for Icon is 40.");
        }
    }

    public class FeelingTypeUpdateValidator : AbstractValidator<FeelingTypeUpdateDTO>
    {
        public FeelingTypeUpdateValidator(BuzzUpContext ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(30).WithMessage("Maximum length for Name is 30.")
                .Must((dto, name) => !ctx.FeelingTypes.Any(f => f.Name == name && f.Id != dto.Id && f.IsActive && f.DeletedAt == null))
                .WithMessage("Name already exists.");
            RuleFor(x => x.Icon)
                .NotEmpty().WithMessage("Icon is required.")
                .MaximumLength(40).WithMessage("Maximum length for Icon is 40.");
        }
    }
}
