using AutoMapper;
using AutoMapper.QueryableExtensions;
using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Comments;
using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.UseCases.Queries.Comments;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;

namespace BuzzUp_API.Implementation.UseCases.Queries.Comments
{
    public class EfGetCommentsQuery : EfUseCaseMapper, IGetCommentsQuery
    {
        private readonly IApplicationActor _actor;

        public EfGetCommentsQuery(BuzzUpContext context, IMapper mapper, IApplicationActor actor) : base(context, mapper)
        {
            _actor = actor;
        }

        public int Id => 27;

        public string Name => "Get Comments";

        public List<CommentDTO> Execute(int postId)
        {
            var post = Context.Posts
                .Where(p => p.Id == postId && p.IsActive && p.DeletedAt == null)
                .Select(p => new
                {
                    p.Id,
                    p.UserId,
                    VisibilityName = p.VisibilityType.Name
                })
                .FirstOrDefault();

            if (post == null || !CanView(post.UserId, post.VisibilityName))
            {
                throw new EntityNotFoundException(nameof(Post), postId);
            }

            var commentsOnPost = Context.Comments
                .Where(comment => comment.PostId == postId)
                .Where(comment => comment.IsActive && comment.DeletedAt == null)
                .Where(comment => comment.User.IsActive && comment.User.DeletedAt == null)
                .OrderBy(comment => comment.CreatedAt)
                .ProjectTo<CommentDTO>(Mapper.ConfigurationProvider)
                .ToList();

            return NestComments(commentsOnPost);
        }

        private static List<CommentDTO> NestComments(List<CommentDTO> commentsOnPost)
        {
            foreach (var comment in commentsOnPost)
            {
                comment.Replies = new List<CommentDTO>();
            }

            var commentsById = commentsOnPost.ToDictionary(comment => comment.Id);
            var rootComments = new List<CommentDTO>();

            foreach (var comment in commentsOnPost)
            {
                if (!comment.ParentId.HasValue)
                {
                    rootComments.Add(comment);
                    continue;
                }

                if (commentsById.TryGetValue(comment.ParentId.Value, out var parentComment))
                {
                    parentComment.Replies.Add(comment);
                }
            }

            return rootComments;
        }

        private bool CanView(int authorUserId, string visibilityName)
        {
            if (authorUserId == _actor.Id)
            {
                return true;
            }

            if (visibilityName == "Public")
            {
                return true;
            }

            if (visibilityName == "Friends")
            {
                return Context.Friendships.Any(friendship =>
                    friendship.IsActive &&
                    friendship.DeletedAt == null &&
                    friendship.FriendRequestStatus.Name == "Accepted" &&
                    ((friendship.SenderUserId == _actor.Id && friendship.ReceiverUserId == authorUserId) ||
                     (friendship.ReceiverUserId == _actor.Id && friendship.SenderUserId == authorUserId)));
            }

            return false;
        }
    }
}
