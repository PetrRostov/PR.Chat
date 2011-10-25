using System;
using System.IO;
using System.Reflection;
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
        private static Configuration _configuration;
        private static ISessionFactory _sessionFactory;
        protected ISession Session;

        public BaseMappingFixtures(Assembly assemblyContainingMapping)
        {
            if (_configuration == null)
            {
                _configuration = new Configuration()
                    .SetProperty(Environment.ReleaseConnections, "on_close")
                    .SetProperty(Environment.Dialect, typeof(SQLiteDialect).AssemblyQualifiedName)
                    .SetProperty(Environment.ConnectionDriver, typeof(SQLite20Driver).AssemblyQualifiedName)
                    .SetProperty(Environment.ConnectionString, "data source=:memory:")
                    .SetProperty(Environment.ProxyFactoryFactoryClass, typeof(DefaultProxyFactoryFactory).AssemblyQualifiedName)
                    .SetProperty(Environment.ShowSql, "true")
                    .AddAssembly(assemblyContainingMapping);

                _sessionFactory = _configuration.BuildSessionFactory();
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

        [TestFixtureTearDown]
        public void Dispose()
        {
            Session.Dispose();
        }
    }
}