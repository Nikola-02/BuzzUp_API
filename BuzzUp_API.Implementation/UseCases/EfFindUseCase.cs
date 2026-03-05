using AutoMapper;
using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.Repository;
using BuzzUp_API.Application.UseCases;
using BuzzUp_API.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases
{
    public abstract class EfFindUseCase<TResult, TEntity> : EfUseCase<TEntity>, IQuery<TResult, int>
        where TEntity : class
        where TResult : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        protected EfFindUseCase(IRepository<TEntity> repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public abstract int Id { get; }
        public abstract string Name { get; }

        public TResult Execute(int id)
        {
            var item = Repository.Find(id);

            if(item == null)
            {
                throw new EntityNotFoundException(nameof(TEntity), id);
            }

            return _mapper.Map<TResult>(item);
        }
    }
}
