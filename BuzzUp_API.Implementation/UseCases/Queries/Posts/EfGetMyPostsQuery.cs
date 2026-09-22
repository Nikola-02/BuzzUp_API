using AutoMapper;
using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.DTO.Posts;
using BuzzUp_API.Application.UseCases.Queries.Posts;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases.Queries.Posts
{
    public class EfGetMyPostsQuery : EfUseCaseMapper, IGetMyPostsQuery
    {
        private readonly IApplicationActor _actor;

        public EfGetMyPostsQuery(BuzzUpContext context, IMapper mapper, IApplicationActor actor) : base(context, mapper)
        {
            _actor = actor;
        }

        public int Id => 15;

        public string Name => "Search My Posts";

        public PagedResponse<PostDTO> Execute(PostSearch search)
        {
            var query = Context.Posts
                .Where(x => x.IsActive && x.DeletedAt == null && x.UserId == _actor.Id)
                .OrderByDescending(x => x.CreatedAt)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Title.Contains(search.Keyword) ||
                                         (x.Description != null && x.Description.Contains(search.Keyword)));
            }

            return query.AsPagedReponse<Post, PostDTO>(search, Mapper);
        }
    }
}
