using AutoMapper;
using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.Application.UseCases;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases
{
    public abstract class EfSearchLookupUseCase<TResult, TEntity> : EfUseCaseMapper, IQuery<PagedResponse<TResult>, TablesSearch>
        where TResult : class
        where TEntity : NamedEntity
    {
        protected EfSearchLookupUseCase(BuzzUpContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public abstract int Id { get; }
        public abstract string Name { get; }

        public PagedResponse<TResult> Execute(TablesSearch search)
        {
            var query = Context.Set<TEntity>().AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.Contains(search.Keyword));
            }

            return query.AsPagedReponse<TEntity, TResult>(search, Mapper);
        }
    }

    public interface IHasName
    {
        string Name { get; set; }
    }
}
