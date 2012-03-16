using System;
using Moq;
using NUnit.Framework;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Data.Tests
{
    [TestFixture, Category(TestCategory.Infrastructure)]
    public class UnitOfWorkFixtures
    {
        private Mock<IDatabase> database;

        [TestFixtureSetUp]
        public void Init()
        {
            database = new Mock<IDatabase>();
            database.Setup(d => d.SubmitChanges()).Verifiable();
            database.Setup(d => d.BeginTransaction()).Verifiable();
            database.Setup(d => d.Rollback()).Verifiable();
        }


        [Test]
        public void Constructor_should_throw_exception_if_arguments_are_null()
        {
            Assert.Throws<ArgumentNullException>(() => new UnitOfWork((IDatabaseFactory)null));
            Assert.Throws<ArgumentNullException>(() => new UnitOfWork((IDatabase)null));
        }

        [Test]
        public void Contructor_should_use_IDatabase_BeginTransaction()
        {
            var unitOfWork = new UnitOfWork(database.Object);
            database.Verify(d => d.BeginTransaction());
        }

        [Test]
        public void Commit_should_use_IDatabase_Submit()
        {
            var unitOfWork = new UnitOfWork(database.Object);
            unitOfWork.Commit();
            database.Verify(d => d.SubmitChanges());
        }

        [Test]
        public void Commit_should_throw_exception_if_UnitOfWork_disposed()
        {
            var unitOfWork = new UnitOfWork(database.Object);
            unitOfWork.Dispose();
            Assert.Throws<ObjectDisposedException>(unitOfWork.Commit);
        }

        [Test]
        public void Commit_should_throw_exception_if_called_second_time()
        {
            var unitOfWork = new UnitOfWork(database.Object);
            unitOfWork.Commit();
            Assert.Throws<ObjectDisposedException>(unitOfWork.Commit);
        }

        [Test]
        public void Dispose_should_use_IDatabase_Rollback()
        {
            var unitOfWork = new UnitOfWork(database.Object);
            unitOfWork.Dispose();
            database.Verify(d => d.Rollback());
        }

    }
}