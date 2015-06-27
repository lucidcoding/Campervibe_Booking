using CampervibeBooking.Data.Core;
using CampervibeBooking.Domain.Common;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CampervibeBooking.Data.Common
{
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : struct
    {
        protected readonly IContextProvider ContextProvider;

        protected Repository(IContextProvider contextProvider)
        {
            ContextProvider = contextProvider;
        }

        protected Context Context
        {
            get { return ContextProvider.GetContext(); }
        }

        protected DbSet<TEntity> DbSet
        {
            get { return Context.Set<TEntity>(); }
        }

        public virtual void Save(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual TEntity GetById(TId id)
        {
            return DbSet.Find(id);
        }

        public virtual IList<TEntity> GetAll()
        {
            IQueryable<TEntity> query = DbSet;
            return query.ToList();
        }

        public virtual IList<TEntity> GetAllUndeleted()
        {
            IQueryable<TEntity> query = DbSet;
            return query
                .Where(entity => !entity.Deleted)
                .ToList();
        }
    }
}

