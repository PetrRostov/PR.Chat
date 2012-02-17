using System;
using NHibernate;

namespace PR.Chat.Infrastructure.Data.NH
{
    public class PerSessionFactoryNHibernateDatabaseFactory : IDatabaseFactory, IDisposable
    {
        private readonly ISessionFactory _sessionFactory;
        private volatile IDatabase _database;
        private object _databaseLock = new object();

        public PerSessionFactoryNHibernateDatabaseFactory(ISessionFactory sessionFactory)
        {
            Check.NotNull(sessionFactory, "sessionFactory");
            _sessionFactory = sessionFactory;
        }

        public PerSessionFactoryNHibernateDatabaseFactory(INHibernateDatabaseConfigurator configurator)
        {
            Check.NotNull(configurator, "configurator");
            _sessionFactory = configurator.GetConfiguration().BuildSessionFactory();
        }

        public PerSessionFactoryNHibernateDatabaseFactory(ISessionFactoryFactory sessionFactoryFactory)
        {
            Check.NotNull(sessionFactoryFactory, "sessionFactoryFactory");
            
            _sessionFactory = sessionFactoryFactory.Create();
        }

        public IDatabase Create()
        {
            if (_database == null)
            {
                lock (_databaseLock)
                {
                    if (_database == null)
                        _database = new PerSessionFactoryNHibernateDatabase(_sessionFactory);
                }
            }
            return _database;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (_database != null)
                _database.Dispose();
        }

        ~PerSessionFactoryNHibernateDatabaseFactory()
        {
            Dispose(false);
        }
    }
}