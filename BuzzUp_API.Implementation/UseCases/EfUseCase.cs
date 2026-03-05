using BuzzUp_API.Application.Repository;
using BuzzUp_API.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases
{
    public abstract class EfUseCase<TEntity>
        where TEntity : class
    {
        protected readonly IRepository<TEntity> Repository;

        protected EfUseCase(IRepository<TEntity> repository)
        {
            Repository = repository;
        }
    }
}
