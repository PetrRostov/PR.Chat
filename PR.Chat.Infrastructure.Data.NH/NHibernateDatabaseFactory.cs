using System;
using NHibernate;

namespace PR.Chat.Infrastructure.Data.NH
{
    public class NHibernateDatabaseFactory : IDatabaseFactory, IDisposable
    {
        private readonly INHibernateDatabaseConfigurator _configurator;
        private readonly ISessionFactory _sessionFactory;
        private IDatabase _database;

        public NHibernateDatabaseFactory(ISessionFactory sessionFactory)
        {
            Check.NotNull(sessionFactory, "sessionFactory");
            _sessionFactory = sessionFactory;
        }

        public NHibernateDatabaseFactory(INHibernateDatabaseConfigurator configurator)
        {
            Check.NotNull(configurator, "configurator");
            _sessionFactory = configurator.GetConfiguration().BuildSessionFactory();
        }

        public IDatabase Create()
        {
            return _database ?? 
                (_database = new NHibernateDatabase(_sessionFactory.OpenSession()));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (_sessionFactory != null)
                _sessionFactory.Dispose();

            if (_database != null)
                _database.Dispose();
        }

        ~NHibernateDatabaseFactory()
        {
            Dispose(false);
        }
    }
}