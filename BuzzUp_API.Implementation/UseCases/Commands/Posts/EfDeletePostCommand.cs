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
            if (_actor.Role == "Admin")
            {
                return;
            }

            if (entity.UserId != _actor.Id)
            {
                throw new ForbiddenException("You can only delete your own post.");
            }
        }

        protected override void AfterDelete(Post entity)
        {
            var deletedAt = DateTime.UtcNow;

            var commentsOnPost = Context.Comments
                .Where(comment => comment.PostId == entity.Id && comment.IsActive && comment.DeletedAt == null)
                .ToList();
            foreach (var comment in commentsOnPost)
            {
                comment.IsActive = false;
                comment.DeletedAt = deletedAt;
            }

            var reactionsOnPost = Context.Reactions
                .Where(reaction => reaction.PostId == entity.Id && reaction.IsActive && reaction.DeletedAt == null)
                .ToList();
            foreach (var reaction in reactionsOnPost)
            {
                reaction.IsActive = false;
                reaction.DeletedAt = deletedAt;
            }

            var notificationsOnPost = Context.Notifications
                .Where(notification => notification.PostId == entity.Id && notification.IsActive && notification.DeletedAt == null)
                .ToList();
            foreach (var notification in notificationsOnPost)
            {
                notification.IsActive = false;
                notification.DeletedAt = deletedAt;
            }
        }
    }
}
