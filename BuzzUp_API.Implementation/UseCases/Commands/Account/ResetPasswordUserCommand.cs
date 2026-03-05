using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.Application.UseCases.Commands.Account;
using BuzzUp_API.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases.Commands.Account
{
    public class ResetPasswordUserCommand : EfUseCase, IResetPasswordUserCommand
    {
        public ResetPasswordUserCommand(BuzzUpContext context) : base(context)
        {
        }

        public int Id => 3;

        public string Name => "ResetPasswordUser";

        public void Execute(ResetPasswordDto data)
        {
            var tokenEntity = Context.PasswordResetTokens.FirstOrDefault(x => x.Token == data.Token);

            if (tokenEntity == null)
                throw new Exception("Token not found.");

            var user = Context.Users.Find(tokenEntity.UserId);
            if (user == null)
                throw new Exception("Users not found.");

            user.Password = BCrypt.Net.BCrypt.HashPassword(data.NewPassword);

            tokenEntity.IsUsed = true;

            Context.SaveChanges();
        }
    }
}
