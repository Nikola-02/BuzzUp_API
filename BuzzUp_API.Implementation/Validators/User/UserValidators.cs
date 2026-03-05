using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.Validators.User
{
    public class UserInsertValidator : AbstractValidator<UserInsertDTO>
    {
        public UserInsertValidator(BuzzUpContext ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Email is not in right format.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("FirstName is required.")
                .MinimumLength(2)
                .WithMessage("Minimum length for FirstName is 2.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastName is required.")
                .MinimumLength(2)
                .WithMessage("Minimum length for LastName is 2.");

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.");

            //Ako je must false onda ce vratiti gresku withMessage
            RuleFor(x => x.Email)
                .Must(x => !ctx.Users.Any(u => u.Email == x && u.IsActive && u.DeletedAt == null))
                .WithMessage("Email is already in use.");

            RuleFor(x => x.Username)
                .Must(x => !ctx.Users.Any(u => u.Username == x && u.IsActive && u.DeletedAt == null))
                .WithMessage("Username is already in use.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.DateOfBirth)
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Date of birth cannot be in the future.")
                .When(x => x.DateOfBirth.HasValue);
        }
    }

    public class UserUpdateValidator : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateValidator(BuzzUpContext ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Email is not in right format.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("FirstName is required.")
                .MinimumLength(2)
                .WithMessage("Minimum length for FirstName is 2.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastName is required.")
                .MinimumLength(2)
                .WithMessage("Minimum length for LastName is 2.");

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.");

            //Ako je must false onda ce vratiti gresku withMessage
            RuleFor(x => x.Email)
                .Must((dto, x) => !ctx.Users.Any(u => u.Email == x && u.IsActive && u.DeletedAt == null && u.Id != dto.Id))
                .WithMessage("Email is already in use.");

            RuleFor(x => x.Username)
                .Must((dto, x) => !ctx.Users.Any(u => u.Username == x && u.IsActive && u.DeletedAt == null && u.Id != dto.Id))
                .WithMessage("Username is already in use.");

            RuleFor(x => x.Password)
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.")
                .When(x => !string.IsNullOrWhiteSpace(x.Password));

            RuleFor(x => x.DateOfBirth)
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Date of birth cannot be in the future.")
                .When(x => x.DateOfBirth.HasValue);
        }
    }
    
}
