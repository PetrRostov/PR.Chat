using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace PR.Chat.Infrastructure.Data.NH
{
    public abstract class NHibernateDatabaseBase : IDatabase
    {
        protected abstract ISession GetSession();


        #region Implementation of IDatabase

        public virtual IQueryable<T> GetSource<T, TKey>() where T : class, IEntity<T, TKey>
        {
            return GetSession().Query<T>();
        }

        public virtual void UpdateOnSubmit<T, TKey>(IEntity<T, TKey> entity)
        {
            GetSession().Update(entity);
        }

        public virtual void DeleteOnSubmit<T, TKey>(IEntity<T, TKey> entity)
        {
            GetSession().Delete(entity);
        }

        public virtual TKey AddOnSubmit<T, TKey>(IEntity<T, TKey> entity)
        {
            var key = GetSession().Save(entity);
            return (TKey)key;
        }

        public abstract void SubmitChanges();

        public abstract void BeginTransaction();

        #endregion

        #region Implementation of IDisposable

        public abstract void Dispose();
        #endregion

    }
}