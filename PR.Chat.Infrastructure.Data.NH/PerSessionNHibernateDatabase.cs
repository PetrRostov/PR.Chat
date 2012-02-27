using System;
using NHibernate;
using NHibernate.Linq;

namespace PR.Chat.Infrastructure.Data.NH
{
    public class PerSessionNHibernateDatabase : NHibernateDatabaseBase
    {
        private readonly ISession _session;

        public PerSessionNHibernateDatabase(ISessionFactory sessionFactory)
        {
            Require.NotNull(sessionFactory, "sessionFactory");
            _session = sessionFactory.OpenSession();
        }

        public PerSessionNHibernateDatabase(ISession session)
        {
            Require.NotNull(session, "session");
            _session = session;
        }

        protected override ISession GetSession()
        {
            return _session;
        }

        
        public override void SubmitChanges()
        {
            var transaction = GetSession().Transaction;
            if (transaction != null && transaction.IsActive)
                transaction.Commit();
            else
                throw new TransactionInactiveException();
        }

        public override void BeginTransaction()
        {
            var transaction = GetSession().Transaction;
            if (transaction != null && transaction.IsActive)
                throw new TransactionAlreadyStartedException();

            _session.BeginTransaction();
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposed)
        {
            if (disposed)
                _session.Dispose();
        }

        ~PerSessionNHibernateDatabase()
        {
            Dispose(false);
        }
    }
}