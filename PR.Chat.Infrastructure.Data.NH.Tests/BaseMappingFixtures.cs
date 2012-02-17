using System;
using System.IO;
using System.Reflection;
using System.Threading;
using NHibernate;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Environment = NHibernate.Cfg.Environment;

namespace PR.Chat.Infrastructure.Data.NH.Tests
{
    public class BaseMappingFixtures : IDisposable
    {
        protected static Configuration _configuration;
        protected static ISessionFactory _sessionFactory;
        protected ISession Session;
        protected static Assembly CurrentAssembly;
        private static ReaderWriterLockSlim _configurationLock = new ReaderWriterLockSlim();

        public BaseMappingFixtures(Assembly assemblyContainingMapping)
        {
            _configurationLock.EnterUpgradeableReadLock();
            try
            {
                if (_configuration == null)
                {
                    _configurationLock.EnterWriteLock();
                    try
                    {
                        if (_configuration == null)
                        {
                            CurrentAssembly = assemblyContainingMapping;
                            _configuration = GetSQLiteInMemoryConfiguration()
                                .AddAssembly(assemblyContainingMapping);

                            _sessionFactory = _configuration.BuildSessionFactory();
                        }
                    }
                    finally
                    {
                        _configurationLock.ExitWriteLock();
                    }
                }

                Session = _sessionFactory.OpenSession();

                new SchemaExport(_configuration).Execute(
                    true,
                    true,
                    false,
                    Session.Connection,
                    Console.Out
                );  
            }
            finally
            {
                _configurationLock.ExitUpgradeableReadLock();
            }

        }

        public static Configuration GetSQLiteInMemoryConfiguration()
        {
            return new Configuration()
                .SetProperty(Environment.ReleaseConnections, "on_close")
                .SetProperty(Environment.Dialect, typeof(SQLiteDialect).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionDriver, typeof(SQLite20Driver).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionString, "data source=:memory:")
                .SetProperty(Environment.ProxyFactoryFactoryClass, typeof(DefaultProxyFactoryFactory).AssemblyQualifiedName)
                .SetProperty(Environment.ShowSql, "true");
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            Session.Dispose();
        }
    }
}