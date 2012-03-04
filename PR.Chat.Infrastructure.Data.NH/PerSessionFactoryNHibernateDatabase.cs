using System;
using System.Linq;
using System.Threading;
using NHibernate;

namespace PR.Chat.Infrastructure.Data.NH
{
    public class PerSessionFactoryNHibernateDatabase : NHibernateDatabaseBase
    {

        protected static ThreadLocal<ISession> CurrentSessionHolder = new ThreadLocal<ISession>();
        
        private readonly ISessionFactory _sessionFactory;

        public PerSessionFactoryNHibernateDatabase(ISessionFactory sessionFactory)
        {
            Require.NotNull(sessionFactory, "sessionFactory");
            _sessionFactory = sessionFactory;
        }

        #region Overrides of NHibernateDatabaseBase

        protected override ISession GetSession()
        {
            return CurrentSessionHolder.Value ?? (CurrentSessionHolder.Value = _sessionFactory.OpenSession());
        }

        protected void DisposeSessionAndSetNull()
        {
            if (CurrentSessionHolder.Value != null)
            {
                CurrentSessionHolder.Value.Dispose();
                CurrentSessionHolder.Value = null;
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
            if (disposing)
            {
                if (_sessionFactory != null)
                    _sessionFactory.Dispose();

                if (CurrentSessionHolder != null)
                    CurrentSessionHolder.Dispose();
            }
        }

        ~PerSessionFactoryNHibernateDatabase()
        {
            Dispose(false);
        }

        #endregion
    }
}