using AutoMapper;
using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.Application.UseCases.Commands.Users;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases.Commands.Users
{
    public class EfUpdateUserCommand : EfUpdateUseCase<UserUpdateDTO, User>, IUpdateUserCommand
    {
        public EfUpdateUserCommand(BuzzUpContext context, IMapper mapper, IValidator<UserUpdateDTO> validator) : base(context, mapper, validator)
        {
        }

        public override int Id => 5;

        public override string Name => "UpdateUser";
    }
}
