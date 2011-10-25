using System;
using Moq;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NUnit.Framework;
using PR.Chat.Test.Common;
using Environment = NHibernate.Cfg.Environment;

namespace PR.Chat.Infrastructure.Data.NH.Tests
{
    [TestFixture, Category(TestCategory.Infrastructure)]
    public class MsSql2008NHibernateDatabaseConfiguratorFixtures
    {
        [Test]
        public void Constructor_should_throw_exception_if_argument_is_null()
        {
            Assert.Throws<ArgumentNullException>(
                () => new MsSql2008NHibernateDatabaseConfigurator((IConnectionStringProvider) null)
            );
        }


        [Test]
        public void GetConfiguration_should_return_correct_result()
        {
            var connectionString = "ConnectionStringOpa";
            var connectionStringProvider = new Mock<IConnectionStringProvider>();
            connectionStringProvider.Setup(c => c.GetConnectionString()).Returns(connectionString);

            var configurator = new MsSql2008NHibernateDatabaseConfigurator(connectionStringProvider.Object);

            var configuration = configurator.GetConfiguration();
            Assert.IsNotNull(configuration);

            Assert.AreEqual(
                GetProperty(configuration, Environment.ConnectionDriver), 
                typeof(Sql2008ClientDriver).AssemblyQualifiedName
            );

            Assert.AreEqual(
                GetProperty(configuration, Environment.ProxyFactoryFactoryClass),
                typeof(DefaultProxyFactoryFactory).AssemblyQualifiedName
            );

            Assert.AreEqual(
                GetProperty(configuration, Environment.Dialect),
                typeof(MsSql2008Dialect).AssemblyQualifiedName
            );

            Assert.AreEqual(
                GetProperty(configuration, Environment.ConnectionString),
                connectionString
            );

        }

        private string GetProperty(Configuration configuration, string propertyName)
        {
            return configuration.GetProperty(propertyName);
        }
    }
}