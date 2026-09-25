using AutoMapper;
using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Posts;
using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.UseCases.Queries.Posts;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;

namespace BuzzUp_API.Implementation.UseCases.Queries.Posts
{
    public class EfGetSinglePostQuery : EfFindUseCase<PostDTO, Post>, IGetSinglePostQuery
    {
        private readonly IApplicationActor _actor;

        public EfGetSinglePostQuery(BuzzUpContext context, IMapper mapper, IApplicationActor actor) : base(context, mapper)
        {
            _actor = actor;
        }

        public override int Id => 12;

        public override string Name => "Find Post";

        public override PostDTO Execute(int id)
        {
            var post = Context.Posts.Find(id);

            if (post == null || !post.IsActive || post.DeletedAt != null || !CanView(post))
            {
                throw new EntityNotFoundException(nameof(Post), id);
            }

            var dto = base.Execute(id);
            PostReactionSummary.FillPostsWithViewerReactions(Context, _actor.Id, dto);
            return dto;
        }

        private bool CanView(Post post)
        {
            if (post.UserId == _actor.Id)
            {
                return true;
            }

            if (post.VisibilityType.Name == "Public")
            {
                return true;
            }

            if (post.VisibilityType.Name == "Friends")
            {
                return Context.Friendships.Any(f =>
                    f.IsActive &&
                    f.DeletedAt == null &&
                    f.FriendRequestStatus.Name == "Accepted" &&
                    ((f.SenderUserId == _actor.Id && f.ReceiverUserId == post.UserId) ||
                     (f.ReceiverUserId == _actor.Id && f.SenderUserId == post.UserId)));
            }

            return false;
        }
    }
}
