using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.Application.UseCases.Commands.Account;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases.Commands.Account
{
    public class ForgotPasswordUserCommand : EfUseCase, IForgotPasswordUserCommand
    {
        public ForgotPasswordUserCommand(BuzzUpContext context) : base(context)
        {
        }

        public int Id => 2;

        public string Name => "ForgotPasswordUser";

        public void Execute(ForgotPasswordDto dto)
        {
            var user = Context.Users.FirstOrDefault(x => x.Email == dto.UserEmail);

            if (user == null)
                return;

            var token = ForgotPassword.GenerateToken();

            var resetToken = new PasswordResetToken
            {
                UserId = user.Id,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                IsUsed = false
            };

            Context.PasswordResetTokens.Add(resetToken);
            Context.SaveChanges();

            var resetLink = $"{dto.ResetPasswordUrl}/reset-password?token={Uri.EscapeDataString(token)}";

            try
            {
                ForgotPassword.SendEmail(user.Email, resetLink, dto.SmtpEmail, dto.SmtpPassword);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send reset password email to {user.Email}. Error: {ex.Message}", ex);
            }
        }
    }
}
