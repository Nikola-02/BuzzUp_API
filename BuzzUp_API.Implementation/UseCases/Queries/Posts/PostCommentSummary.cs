using BuzzUp_API.Application.DTO.Posts;
using BuzzUp_API.DataAccess;

namespace BuzzUp_API.Implementation.UseCases.Queries.Posts
{
    internal static class PostCommentSummary
    {
        public static void FillPostsWithCommentCounts(BuzzUpContext context, params PostDTO[] posts)
        {
            FillPostsWithCommentCounts(context, (IEnumerable<PostDTO>)posts);
        }

        public static void FillPostsWithCommentCounts(BuzzUpContext context, IEnumerable<PostDTO> posts)
        {
            var postsToFill = posts.Where(post => post != null).ToList();
            if (postsToFill.Count == 0)
            {
                return;
            }

            var postIds = postsToFill.Select(post => post.Id).ToList();
            var commentCountsByPostId = context.Comments
                .Where(comment => postIds.Contains(comment.PostId) && comment.IsActive && comment.DeletedAt == null)
                .GroupBy(comment => comment.PostId)
                .Select(commentGroup => new
                {
                    PostId = commentGroup.Key,
                    CommentCount = commentGroup.Count()
                })
                .ToList();

            foreach (var postDto in postsToFill)
            {
                var commentCountForPost = commentCountsByPostId
                    .FirstOrDefault(row => row.PostId == postDto.Id);
                postDto.CommentCount = commentCountForPost?.CommentCount ?? 0;
            }
        }
    }
}
