using AutoMapper;
using BuzzUp_API.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation
{
    public static class QueryableExtensions
    {
        public static PagedResponse<TResult> AsPagedReponse<TEntity, TResult>
            (this IQueryable<TEntity> query, PagedSearch search, IMapper mapper)
            where TEntity : class
            where TResult : class
        {

            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            //16 PerPage = 5, Page = 2

            int skip = perPage * (page - 1);

            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<TResult>
            {
                CurrentPage = page,
                Data = mapper.ProjectTo<TResult>(query).ToList(),
                PerPage = perPage,
                TotalCount = totalCount,
            };
        }
    }
}
