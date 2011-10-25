using System.Collections.Generic;
using System.Linq;

namespace PR.Chat.Infrastructure.Data
{
    public abstract class BaseRepository<T, TKey> : IRepository<T, TKey>
        where T : class, IEntity<T, TKey>
    {
        private readonly IDatabase _database;

        protected BaseRepository(IDatabaseFactory databaseFactory)
        {
            Check.NotNull(databaseFactory, "databaseFactory");
            _database = databaseFactory.Create();
        }

        protected BaseRepository(IDatabase database)
        {
            Check.NotNull(database, "database");
            _database = database;
        }

        public virtual void Add(T entity)
        {
            _database.AddOnSubmit(entity);
        }

        public virtual T GetById(TKey key)
        {
            var element = _database.GetSource<T, TKey>()
                .FirstOrDefault(entity => entity.Id.Equals(key));

            if (element == null)
                throw new EntityNotFoundException();

            return element;
        }

        public virtual void Remove(T entity)
        {
            _database.DeleteOnSubmit(entity);
        }

        public virtual void Update(T entity)
        {
            _database.UpdateOnSubmit(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _database.GetSource<T, TKey>().AsEnumerable();
        }
    }
}