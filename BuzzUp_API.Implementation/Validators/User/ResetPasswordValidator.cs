using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.Validators.User
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
    {
        public BuzzUpContext _context { get; set; }
        public ResetPasswordValidator(BuzzUpContext context)
        {
            this._context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.NewPassword)
                        .NotEmpty().WithMessage("Password is required.")
                        .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                        .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                        .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                        .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required")
            .Must(TokenIsValid).WithMessage("Token is invalid or expired");
        }
        private bool TokenIsValid(string token)
        {
            var resetToken = _context.PasswordResetTokens
                .FirstOrDefault(x => x.Token == token);

            if (resetToken == null) return false;
            if (resetToken.IsUsed) return false;
            if (resetToken.ExpiresAt < DateTime.UtcNow) return false;

            return true;
        }
    }

}
