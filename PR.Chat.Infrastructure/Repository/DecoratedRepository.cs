using System.Collections.Generic;
using System.Diagnostics;

namespace PR.Chat.Infrastructure
{
    public class DecoratedRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    {
        protected readonly IRepository<TEntity, TKey> InnerRepository;

        public DecoratedRepository(IRepository<TEntity, TKey> innerRepository)
        {
            Check.NotNull(innerRepository, "innerRepository");
            InnerRepository = innerRepository;
        }

        [DebuggerStepThrough]
        public virtual void Add(TEntity entity)
        {
            InnerRepository.Add(entity);
        }

        [DebuggerStepThrough]
        public virtual TEntity GetById(TKey key)
        {
            return InnerRepository.GetById(key);
        }

        [DebuggerStepThrough]
        public virtual void Remove(TEntity entity)
        {
            InnerRepository.Remove(entity);
        }

        [DebuggerStepThrough]
        public virtual void Update(TEntity entity)
        {
            InnerRepository.Update(entity);
        }

        [DebuggerStepThrough]
        public virtual IEnumerable<TEntity> GetAll()
        {
            return InnerRepository.GetAll();
        }
    }
}