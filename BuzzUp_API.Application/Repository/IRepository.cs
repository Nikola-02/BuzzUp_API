using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Application.Repository
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        TEntity Find(int id);
        IQueryable<TEntity> Query();
        void Add(TEntity entity);
        void Delete(TEntity entity);
    }
}
