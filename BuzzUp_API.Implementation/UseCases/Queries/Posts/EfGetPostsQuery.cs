using AutoMapper;
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
        public EfGetPostsQuery(BuzzUpContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public int Id => 11;

        public string Name => "Search Posts";

        public PagedResponse<PostDTO> Execute(PostSearch search)
        {
            var query = Context.Posts
                .Where(x => x.IsActive && x.DeletedAt == null)
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
