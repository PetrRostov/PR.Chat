using System.Collections.Generic;
using System.Linq;

namespace PR.Chat.Infrastructure.Data
{
    public abstract class BaseRepository<T, TKey> : IRepository<T, TKey>
        where T : class, IEntity<T, TKey>
    {
        protected readonly IDatabase Database;

        protected BaseRepository(IDatabaseFactory databaseFactory)
        {
            Check.NotNull(databaseFactory, "databaseFactory");
            Database = databaseFactory.Create();
        }

        protected BaseRepository(IDatabase database)
        {
            Check.NotNull(database, "database");
            Database = database;
        }

        public virtual void Add(T entity)
        {
            Database.AddOnSubmit(entity);
        }

        public virtual T GetById(TKey key)
        {
            var element = GetSource()
                .FirstOrDefault(entity => entity.Id.Equals(key));

            if (element == null)
                throw new EntityNotFoundException();

            return element;
        }

        public virtual void Remove(T entity)
        {
            Database.DeleteOnSubmit(entity);
        }

        public virtual void Update(T entity)
        {
            Database.UpdateOnSubmit(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return GetSource().AsEnumerable();
        }

        protected virtual IQueryable<T> GetSource()
        {
            return Database.GetSource<T, TKey>();
        }
    }
}