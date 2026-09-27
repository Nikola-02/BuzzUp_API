using AutoMapper;
using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Comments;
using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.UseCases.Commands.Comments;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases;
using FluentValidation;

namespace BuzzUp_API.Implementation.UseCases.Commands.Comments
{
    public class EfUpdateCommentCommand : EfUpdateUseCase<CommentUpdateDTO, Comment>, IUpdateCommentCommand
    {
        private readonly IApplicationActor _actor;

        public EfUpdateCommentCommand(
            BuzzUpContext context,
            IMapper mapper,
            IValidator<CommentUpdateDTO> validator,
            IApplicationActor actor)
            : base(context, mapper, validator)
        {
            _actor = actor;
        }

        public override int Id => 29;

        public override string Name => "Update Comment";

        protected override void EnsureCanUpdate(CommentUpdateDTO request, Comment entity)
        {
            if (entity.UserId != _actor.Id)
            {
                throw new ForbiddenException("You can only edit your own comment.");
            }
        }
    }
}
