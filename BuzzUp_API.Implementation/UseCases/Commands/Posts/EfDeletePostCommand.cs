using BuzzUp_API.Application;
using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.UseCases.Commands.Posts;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases.Commands.Posts
{
    public class EfDeletePostCommand : EfDeleteUseCase<Post>, IDeletePostCommand
    {
        private readonly IApplicationActor _actor;

        public EfDeletePostCommand(BuzzUpContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public override int Id => 14;

        public override string Name => "Delete Post";

        protected override void EnsureCanDelete(Post entity)
        {
            if (entity.UserId != _actor.Id)
            {
                throw new ForbiddenException("You can only delete your own post.");
            }
        }
    }
}
