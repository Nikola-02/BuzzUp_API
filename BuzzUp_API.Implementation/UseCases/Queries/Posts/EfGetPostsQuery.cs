using AutoMapper;
using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.DTO.Posts;
using BuzzUp_API.Application.UseCases.Queries.Posts;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases.Queries.Posts
{
    public class EfGetPostsQuery : EfUseCaseMapper, IGetPostsQuery
    {
        private readonly IApplicationActor _actor;

        public EfGetPostsQuery(BuzzUpContext context, IMapper mapper, IApplicationActor actor) : base(context, mapper)
        {
            _actor = actor;
        }

        public int Id => 11;

        public string Name => "Search Posts";

        public PagedResponse<PostDTO> Execute(PostSearch search)
        {
            var actorId = _actor.Id;

            var query = Context.Posts
                .Where(x => x.IsActive && x.DeletedAt == null)
                .Where(x =>
                    x.UserId == actorId ||
                    x.VisibilityType.Name == "Public" ||
                    (x.VisibilityType.Name == "Friends" && Context.Friendships.Any(f =>
                        f.IsActive &&
                        f.DeletedAt == null &&
                        f.FriendRequestStatus.Name == "Accepted" &&
                        ((f.SenderUserId == actorId && f.ReceiverUserId == x.UserId) ||
                         (f.ReceiverUserId == actorId && f.SenderUserId == x.UserId)))))
                .OrderByDescending(x => x.CreatedAt)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Title.Contains(search.Keyword) ||
                                         (x.Description != null && x.Description.Contains(search.Keyword)));
            }

            if (search.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == search.UserId.Value);
            }

            return query.AsPagedReponse<Post, PostDTO>(search, Mapper);
        }
    }
}
