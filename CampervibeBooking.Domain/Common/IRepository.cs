using System.Collections.Generic;

namespace CampervibeBooking.Domain.Common
{
    public interface IRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : struct
    {
        void Save(TEntity obj);
        void Update(TEntity obj);
        TEntity GetById(TId id);
        IList<TEntity> GetAll();
        IList<TEntity> GetAllUndeleted();
    }
}