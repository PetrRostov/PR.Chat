using System;
using NUnit.Framework;
using PR.Chat.Infrastructure.Data.NH;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Data.Tests
{
    [TestFixture, Category(TestCategory.Infrastructure)]
    public class ConnectionStringProviderFixtures
    {
        [Test]
        public void Constructor_should_throw_ArgumentNullException_If_argument_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new ConnectionStringProvider((string) null));
        }

        [Test]
        public void GetConnectionString_should_return_right_result()
        {
            var connectionStringProvider = new ConnectionStringProvider("TestConnectionString");
            Assert.AreEqual(connectionStringProvider.GetConnectionString(), "ConnectionString");
        }

        [Test]
        public void GetConnectionString_should_throw_exception_if_connection_string_not_found()
        {
            var connectionStringProvider = new ConnectionStringProvider("Opa");
            Assert.Throws<ConnectionStringNotFoundException>(() => connectionStringProvider.GetConnectionString());
        }
    }
}