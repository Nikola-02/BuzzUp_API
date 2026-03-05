using BuzzUp_API.Application.Repository;
using BuzzUp_API.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation
{
    public class EfRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
    {
        protected readonly BuzzUpContext Context;
        protected readonly DbSet<TEntity> Set;

        public EfRepository(BuzzUpContext context)
        {
            Context = context;
            Set = context.Set<TEntity>();
        }

        public TEntity Find(int id)
        {
            return Set.Find(id);
        }

        public IQueryable<TEntity> Query()
        {
            return Set.AsQueryable();
        }

        public void Add(TEntity entity)
        {
            Set.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            Set.Remove(entity);
        }
    }
}
