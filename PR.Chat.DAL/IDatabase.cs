using System;
using System.Collections.Generic;
using System.Linq;
using PR.Chat.Core.BusinessObjects;

namespace PR.Chat.DAL
{
    public interface IDatabase : IDisposable
    {
        IQueryable<TEntity> GetSource<TEntity>() where TEntity : class, IEntity;

        void DeleteOnSubmit(IEntity entity);

        void AddOnSubmit(IEntity entity);

        void AddAllOnSubmit(IEnumerable<IEntity> entities);

        void SubmitChanges();
    }
}