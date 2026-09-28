using BuzzUp_API.Application.DTO.Reactions;
using BuzzUp_API.DataAccess;
using FluentValidation;

namespace BuzzUp_API.Implementation.Validators.Lookup
{
    public class ReactionTypeInsertValidator : AbstractValidator<ReactionTypeInsertDTO>
    {
        public ReactionTypeInsertValidator(BuzzUpContext ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(30).WithMessage("Maximum length for Name is 30.")
                .Must(name => !ctx.ReactionTypes.Any(r => r.Name == name && r.IsActive && r.DeletedAt == null))
                .WithMessage("Name already exists.");
            RuleFor(x => x.Icon)
                .NotEmpty().WithMessage("Icon is required.")
                .MaximumLength(40).WithMessage("Maximum length for Icon is 40.");
        }
    }

    public class ReactionTypeUpdateValidator : AbstractValidator<ReactionTypeUpdateDTO>
    {
        public ReactionTypeUpdateValidator(BuzzUpContext ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(30).WithMessage("Maximum length for Name is 30.")
                .Must((dto, name) => !ctx.ReactionTypes.Any(r => r.Name == name && r.Id != dto.Id && r.IsActive && r.DeletedAt == null))
                .WithMessage("Name already exists.");
            RuleFor(x => x.Icon)
                .NotEmpty().WithMessage("Icon is required.")
                .MaximumLength(40).WithMessage("Maximum length for Icon is 40.");
        }
    }
}
