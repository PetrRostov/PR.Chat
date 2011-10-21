using System.Collections.Generic;

namespace PR.Chat.Infrastructure
{
    public interface IRepository<TEntity, in TKey> 
    {
        void Add(TEntity entity);

        TEntity GetById(TKey key);

        void Remove(TEntity entity);

        void Update(TEntity entity);

        IEnumerable<TEntity> GetAll();
    }
}