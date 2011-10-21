using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace PR.Chat.Infrastructure.Data.NH
{
    public class NHibernateDatabase : IDatabase
    {
        private readonly ISession _session;

        public NHibernateDatabase(ISessionFactory sessionFactory)
        {
            Check.NotNull(sessionFactory, "sessionFactory");
            _session = sessionFactory.OpenSession();
        }

        public NHibernateDatabase(ISession session)
        {
            Check.NotNull(session, "session");
            _session = session;
        }

        public IQueryable<T> GetSource<T, TKey>() where T : class, IEntity<T, TKey>
        {
            return _session.Query<T>();
        }

        public void UpdateOnSubmit<T, TKey>(IEntity<T, TKey> entity)
        {
            _session.Update(entity);
        }

        public void DeleteOnSubmit<T, TKey>(IEntity<T, TKey> entity)
        {
            _session.Delete(entity);
        }

        public TKey AddOnSubmit<T, TKey>(IEntity<T, TKey> entity)
        {
            var key = _session.Save(entity);
            return (TKey)key;
        }

        public void Submit()
        {
            var transaction = _session.Transaction;
            if (transaction != null && transaction.IsActive)
                transaction.Commit();
            else
                throw new TransactionInactiveException();
        }

        public void BeginTransaction()
        {
            var transaction = _session.Transaction;
            if (transaction != null && transaction.IsActive)
                throw new TransactionAlreadyStartedException();

            _session.BeginTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposed)
        {
            if (disposed)
                _session.Dispose();
        }

        ~NHibernateDatabase()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }
    }
}