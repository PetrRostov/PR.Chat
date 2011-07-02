using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using PR.Chat.Core.BusinessObjects;
using PR.Chat.Core.Repositories;

namespace PR.Chat.DAL.Repositories
{
    public class BaseRepository<TEntity, TEntityImpl> : IRepository<TEntity> 
        where TEntity : IEntity
        where TEntityImpl : class, TEntity
    {
        protected readonly IDatabase Database;

        public BaseRepository(IDatabaseFactory databaseFactory)
        {
            Contract.Requires<ArgumentException>(databaseFactory != null);

            Database = databaseFactory.Create();
        }

        protected IQueryable<TEntityImpl> GetSource()
        {
            return Database.GetSource<TEntityImpl>();
        }

        public BaseRepository(IDatabase database)
        {
            Contract.Requires<ArgumentException>(database != null);

            Database = database;
        }


        public void Add(TEntity entity)
        {
            Database.AddOnSubmit(entity);
        }

        public void Delete(TEntity entity)
        {
            Database.DeleteOnSubmit(entity);
        }

        public void AddAll(IEnumerable<TEntity> entities)
        {
            Database.AddAllOnSubmit((IEnumerable <IEntity>) entities);
        }
    }
}