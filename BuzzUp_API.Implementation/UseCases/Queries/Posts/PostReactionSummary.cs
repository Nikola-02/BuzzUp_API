using BuzzUp_API.Application.DTO.Posts;
using BuzzUp_API.DataAccess;

namespace BuzzUp_API.Implementation.UseCases.Queries.Posts
{
    internal static class PostReactionSummary
    {
        public static void FillPostsWithViewerReactions(BuzzUpContext context, int viewerUserId, params PostDTO[] posts)
        {
            FillPostsWithViewerReactions(context, viewerUserId, (IEnumerable<PostDTO>)posts);
        }

        public static void FillPostsWithViewerReactions(BuzzUpContext context, int viewerUserId, IEnumerable<PostDTO> posts)
        {
            var postsToFill = posts.Where(post => post != null).ToList();
            if (postsToFill.Count == 0)
            {
                return;
            }

            var postIds = postsToFill.Select(post => post.Id).ToList();
            var reactionsOnThosePosts = context.Reactions
                .Where(reaction => postIds.Contains(reaction.PostId) && reaction.IsActive && reaction.DeletedAt == null)
                .Select(reaction => new
                {
                    reaction.PostId,
                    reaction.UserId,
                    reaction.ReactionTypeId,
                    reaction.ReactionType.Name,
                    reaction.ReactionType.Icon
                })
                .ToList();

            foreach (var postDto in postsToFill)
            {
                var reactionsOnThisPost = reactionsOnThosePosts
                    .Where(reaction => reaction.PostId == postDto.Id)
                    .ToList();
                var viewerReaction = reactionsOnThisPost
                    .FirstOrDefault(reaction => reaction.UserId == viewerUserId);

                postDto.ReactionCount = reactionsOnThisPost.Count;
                postDto.MyReactionTypeId = viewerReaction?.ReactionTypeId;
                postDto.MyReactionName = viewerReaction?.Name;
                postDto.MyReactionIcon = viewerReaction?.Icon;
                postDto.UsedReactionTypeIds = reactionsOnThisPost
                    .Select(reaction => reaction.ReactionTypeId)
                    .Distinct()
                    .OrderBy(reactionTypeId => reactionTypeId)
                    .ToList();
            }
        }
    }
}
