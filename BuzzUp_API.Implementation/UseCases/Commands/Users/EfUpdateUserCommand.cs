using AutoMapper;
using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.Application.Exceptions;
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
        private readonly IApplicationActor _actor;

        public EfUpdateUserCommand(BuzzUpContext context, IMapper mapper, IValidator<UserUpdateDTO> validator, IApplicationActor actor)
            : base(context, mapper, validator)
        {
            _actor = actor;
        }

        public override int Id => 5;

        public override string Name => "Update User";

        protected override void EnsureCanUpdate(UserUpdateDTO request, User entity)
        {
            if (_actor.Role == "Admin")
            {
                return;
            }

            if (entity.Id != _actor.Id)
            {
                throw new ForbiddenException("You can only update your own profile.");
            }
        }
    }
}
