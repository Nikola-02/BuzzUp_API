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
    public class BaseUserValidator : AbstractValidator<UserInsertUpdateDTO>
    {
        public BaseUserValidator(BuzzUpContext ctx)
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

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.");

        }

        public class UserUpdateValidator : BaseUserValidator
        {
            public UserUpdateValidator(BuzzUpContext ctx) : base(ctx)
            {
                //Ako je must false onda ce vratiti gresku withMessage
                RuleFor(x => x.Email)
                    .Must((dto, x) => !ctx.Users.Any(u => u.Email == x && u.IsActive && u.DeletedAt == null && u.Id != dto.Id))
                    .WithMessage("Email is already in use.");

                RuleFor(x => x.Username)
                    .Must((dto,x) => !ctx.Users.Any(u => u.Username == x && u.IsActive && u.DeletedAt == null && u.Id != dto.Id))
                    .WithMessage("Username is already in use.");
            }
        }

        public class UserInsertValidator : BaseUserValidator
        {
            public UserInsertValidator(BuzzUpContext ctx) : base(ctx)
            {
                //Ako je must false onda ce vratiti gresku withMessage
                RuleFor(x => x.Email)
                    .Must(x => !ctx.Users.Any(u => u.Email == x && u.IsActive && u.DeletedAt == null))
                    .WithMessage("Email is already in use.");

                RuleFor(x => x.Username)
                    .Must(x => !ctx.Users.Any(u => u.Username == x && u.IsActive && u.DeletedAt == null))
                    .WithMessage("Username is already in use.");
            }
        }
    }
}
