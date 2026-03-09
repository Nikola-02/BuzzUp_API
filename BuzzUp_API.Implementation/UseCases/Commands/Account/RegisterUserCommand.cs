using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.Application.UseCases.Commands.Account;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.Validators.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases.Commands.Account
{
    public class RegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        private UserInsertValidator _validator;
        public RegisterUserCommand(BuzzUpContext context, UserInsertValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 1;

        public string Name => "Register User";

        public void Execute(UserInsertDTO data)
        {
            _validator.ValidateAndThrow(data);

            User user = new User()
            {
                Username = data.Username,
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                Image = string.IsNullOrWhiteSpace(data.Image) ? "default.png" : data.Image,
                Country = data.Country,
                City = data.City,
                Workplace = data.Workplace,
                University = data.University,
                Bio = data.Bio,
                Website = data.Website,
                DateOfBirth = data.DateOfBirth,
                UseCases = new List<UserUseCase>()
                {
                    new UserUseCase() { UseCaseId = 4 },
                },
                RoleId = Context.Roles.FirstOrDefault(r => r.Name == "User").Id
            };

            Context.Users.Add(user);

            Context.SaveChanges();
        }
    }
}
