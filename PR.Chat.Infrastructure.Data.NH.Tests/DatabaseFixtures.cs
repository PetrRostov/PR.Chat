using System;
using Moq;
using NHibernate;
using NUnit.Framework;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Data.NH.Tests
{
    [TestFixture, Category(TestCategory.Infrastructure)]
    public class DatabaseFixtures
    {
        private Mock<ISession> _session;
        private Mock<ITransaction> _transaction;
        private IDatabase _database;
        private TestEntity _testEntity;

        [TestFixtureSetUp]
        public void Init()
        {
            _testEntity = new TestEntity();
            _session = new Mock<ISession>();
            _transaction = new Mock<ITransaction>();

            _session.Setup(s => s.Delete(_testEntity)).Verifiable();
            _session.Setup(s => s.Update(_testEntity)).Verifiable();
            _session.Setup(s => s.BeginTransaction()).Returns(_transaction.Object).Verifiable();
            _session.Setup(s => s.Save(_testEntity)).Returns(Guid.NewGuid()).Verifiable();
            
            _transaction.Setup(t => t.Commit()).Verifiable();

            _database = new NHibernateDatabase(_session.Object);
        }

        [Test]
        public void Constructor_should_throw_exception_if_argument_null()
        {
            Assert.Throws<ArgumentNullException>(() => new NHibernateDatabase((ISession)null));
            Assert.Throws<ArgumentNullException>(() => new NHibernateDatabase((ISessionFactory)null));
        }

        [Test]
        public void BeginTransaction_should_call_ISession_BeginTransaction()
        {
            _session.SetupGet(s => s.Transaction).Returns((ITransaction)null).Verifiable();
            _database.BeginTransaction();
            _session.Verify(s => s.BeginTransaction());
        }

        [Test]
        public void BeginTransaction_should_throw_exception_if_ISession_transacton_active()
        {
            _session.SetupGet(s => s.Transaction).Returns(_transaction.Object).Verifiable();
            _transaction.SetupGet(t => t.IsActive).Returns(true);

            Assert.Throws<TransactionAlreadyStartedException>(() => _database.BeginTransaction());
        }

        [Test]
        public void Submit_should_call_ISession_transaction_commit()
        {
            _session.SetupGet(s => s.Transaction).Returns(_transaction.Object).Verifiable();
            _transaction.SetupGet(t => t.IsActive).Returns(true);

            _database.Submit();
            _session.Verify(s => s.Transaction);
            _transaction.Verify(t => t.IsActive);
            _transaction.Verify(t => t.Commit());
        }

        [Test]
        public void Submit_should_throw_exception_if_inner_transaction_inactive_or_null()
        {
            _session.SetupGet(s => s.Transaction).Returns(_transaction.Object).Verifiable();
            _transaction.SetupGet(t => t.IsActive).Returns(false);
            Assert.Throws<TransactionInactiveException>(() => _database.Submit());

            _session.Verify(s => s.Transaction);
            _transaction.Verify(t => t.IsActive);

            _session.SetupGet(s => s.Transaction).Returns((ITransaction)null);
            Assert.Throws<TransactionInactiveException>(() => _database.Submit());
        }

        [Test]
        public void DeleteOnSubmit_should_call_ISession_Delete()
        {
            _database.DeleteOnSubmit(_testEntity);
            _session.Verify(s => s.Delete(_testEntity), Times.Once());
        }

        [Test]
        public void UpdateOnSubmit_should_call_ISession_Update()
        {
            _database.UpdateOnSubmit(_testEntity);
            _session.Verify(s => s.Update(_testEntity), Times.Once());
        }

        [Test]
        public void AddOnSubmit_should_call_ISession_Add()
        {
            _database.AddOnSubmit(_testEntity);
            _session.Verify(s => s.Save(_testEntity), Times.Once());
        }
    }
}