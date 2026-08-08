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
    public class EfCreateUserCommand : EfCreateUseCase<UserInsertDTO, User>, ICreateUserCommand
    {
        public EfCreateUserCommand(BuzzUpContext context, IMapper mapper, IValidator<UserInsertDTO> validator) : base(context, mapper, validator)
        {
        }

        public override int Id => 8;

        public override string Name => "Admin Create user";
    }
}
