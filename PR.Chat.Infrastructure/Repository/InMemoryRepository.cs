using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PR.Chat.Infrastructure
{
    public class InMemoryRepository<TEntity, TKey> : DecoratedRepository<TEntity, TKey>, IDisposable
        where TEntity : class, IEntity<TEntity, TKey> 
    {
        protected readonly ICollection<TEntity> Entities;
        protected readonly ReaderWriterLockSlim EntitiesLock;

        public InMemoryRepository(IRepository<TEntity, TKey> innerRepository) 
            : base(innerRepository)
        {
            Entities        = new HashSet<TEntity>();
            EntitiesLock    = new ReaderWriterLockSlim();
            LoadEntities();
        }

        protected void LoadEntities()
        {
            GetAll();
        }

        public override void Add(TEntity entity)
        {
            InnerRepository.Add(entity);
            AddToLocalStorage(entity);
        }

        protected void AddToLocalStorage(TEntity entity)
        {
            EntitiesLock.EnterUpgradeableReadLock();
            try
            {
                if (!Entities.Contains(entity))
                {
                    EntitiesLock.EnterWriteLock();
                    try
                    {
                        if (!Entities.Contains(entity))
                        {
                            Entities.Add(entity);
                        }
                    }
                    finally
                    {
                        EntitiesLock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                EntitiesLock.ExitUpgradeableReadLock();
            }
        }

        public override TEntity GetById(TKey key)
        {
            TEntity entity;
            EntitiesLock.EnterReadLock();
            try
            {
                entity = Entities.FirstOrDefault(e => e.Id.Equals(key));
                if (entity != null)
                    return entity;

                entity = InnerRepository.GetById(key);

            }
            finally
            {
                EntitiesLock.ExitReadLock();
            }

            AddToLocalStorage(entity);

            return entity;

        }

        public override void Remove(TEntity entity)
        {
            EntitiesLock.EnterWriteLock();
            try
            {
                Entities.Remove(entity);
            }
            finally
            {
                EntitiesLock.ExitWriteLock();
            }

            base.Remove(entity);
        }

        public override IEnumerable<TEntity> GetAll()
        {
            EntitiesLock.EnterUpgradeableReadLock();
            try
            {
                if (Entities.Count == 0)
                {
                    EntitiesLock.EnterWriteLock();
                    try
                    {
                        if (Entities.Count == 0)
                        {
                            var innerEntities = base.GetAll();

                            innerEntities.ToList().ForEach(Entities.Add);    
                        }
                    }
                    finally
                    {
                        EntitiesLock.ExitWriteLock();
                    }    
                }

                return Entities.ToList().AsReadOnly();
            }
            finally
            {
                EntitiesLock.ExitUpgradeableReadLock();
            }
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (Entities != null)
                Entities.Clear();

            if (InnerRepository  != null)
            {
                var disposableInnerRepository = InnerRepository as IDisposable;
                if (disposableInnerRepository != null)
                    disposableInnerRepository.Dispose();
            }

            if (EntitiesLock != null)
                EntitiesLock.Dispose();
        }

        ~InMemoryRepository()
        {
            Dispose(false);
        }

        #endregion
    }
}