using System.Collections.Generic;
using PR.Chat.Core.BusinessObjects;

namespace PR.Chat.Core.Repositories
{
    public interface IRepository<in TEntity>
        where TEntity : IEntity
    {
        void Add(TEntity entity);

        void Delete(TEntity entity);

        void AddAll(IEnumerable<TEntity> entities);
    }
}