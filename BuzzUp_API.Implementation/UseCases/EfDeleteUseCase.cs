using BuzzUp_API.Application.Exceptions;
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
    public abstract class EfDeleteUseCase<TEntity> : EfUseCase, ICommand<int>
        where TEntity : Entity
    {
        protected EfDeleteUseCase(BuzzUpContext context) : base(context)
        {
        }

        public abstract int Id { get; }
        public abstract string Name { get; }

        public void Execute(int id)
        {
            var entity = Context.Set<TEntity>().FirstOrDefault(x => x.Id == id && x.IsActive && x.DeletedAt == null);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity).Name, id);
            }

            EnsureCanDelete(entity);

            entity.IsActive = false;
            entity.DeletedAt = DateTime.UtcNow;

            Context.SaveChanges();
        }

        protected virtual void EnsureCanDelete(TEntity entity)
        {
        }
    }
}
