using System;
using System.Linq;
using System.Threading;
using NHibernate;

namespace PR.Chat.Infrastructure.Data.NH
{
    public class PerSessionFactoryNHibernateDatabase : NHibernateDatabaseBase
    {
        [ThreadStatic]
        protected static ISession CurrentSession;

        private readonly ISessionFactory _sessionFactory;

        public PerSessionFactoryNHibernateDatabase(ISessionFactory sessionFactory)
        {
            Check.NotNull(sessionFactory, "sessionFactory");
            _sessionFactory = sessionFactory;
        }

        #region Overrides of NHibernateDatabaseBase

        protected override ISession GetSession()
        {
            return CurrentSession ?? (CurrentSession = _sessionFactory.OpenSession());
        }

        protected void DisposeSessionAndSetNull()
        {
            if (CurrentSession != null)
            {
                CurrentSession.Dispose();
                CurrentSession = null;
            }
        }

        public override void SubmitChanges()
        {
            var transaction = GetSession().Transaction;
            if (transaction != null && transaction.IsActive)
                transaction.Commit();
            else
                throw new TransactionInactiveException();

            DisposeSessionAndSetNull();
        }

        public override void BeginTransaction()
        {
            var session = GetSession();

            var transaction = session.Transaction;
            if (transaction != null && transaction.IsActive)
                throw new TransactionAlreadyStartedException();

            session.BeginTransaction();
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing && _sessionFactory != null)
                _sessionFactory.Dispose();
        }

        ~PerSessionFactoryNHibernateDatabase()
        {
            Dispose(false);
        }

        #endregion
    }
}