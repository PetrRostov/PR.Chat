using System;
using Moq;
using NHibernate;
using NUnit.Framework;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Data.NH.Tests
{
    [TestFixture, Category(TestCategory.Infrastructure)]
    public class NHibernateDatabaseFactoryFixtures
    {
        [Test]
        public void Constructor_should_throw_exception_if_arguments_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new PerSessionFactoryNHibernateDatabaseFactory((ISessionFactory) null));
            Assert.Throws<ArgumentNullException>(() => new PerSessionFactoryNHibernateDatabaseFactory((INHibernateDatabaseConfigurator)null));
        }

        [Test]
        public void Create_always_returns_the_same_result()
        {
            var sessionFactory  = new Mock<ISessionFactory>();
            var session         = new Mock<ISession>();

            sessionFactory.Setup(s => s.OpenSession()).Returns(session.Object);

            var databaseFactory = new PerSessionFactoryNHibernateDatabaseFactory(sessionFactory.Object);

            Assert.AreSame(databaseFactory.Create(), databaseFactory.Create());
        }

        [Test]
        public void Create_returns_NHibernateDatabase()
        {
            var sessionFactory = new Mock<ISessionFactory>();
            var session = new Mock<ISession>();

            sessionFactory.Setup(s => s.OpenSession()).Returns(session.Object);

            var databaseFactory = new PerSessionFactoryNHibernateDatabaseFactory(sessionFactory.Object);

            Assert.IsInstanceOf<PerSessionFactoryNHibernateDatabase>(databaseFactory.Create());
        }
    }
}