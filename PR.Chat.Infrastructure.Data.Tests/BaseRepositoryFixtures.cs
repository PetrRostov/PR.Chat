using System;
using System.Linq;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Data.Tests
{
    [TestFixture, Category(TestCategory.Infrastructure)]
    public class BaseRepositoryFixtures
    {
        private Mock<IDatabase> _database;
        private BaseRepositoryImpl _repository;
        private IList<TestEntity> _entities;

        [TestFixtureSetUp]
        public void Init()
        {
            _database = new Mock<IDatabase>();
            _database.Setup(d => d.AddOnSubmit(It.IsAny<IEntity<TestEntity, Guid>>())).Verifiable();
            _database.Setup(d => d.DeleteOnSubmit(It.IsAny<IEntity<TestEntity, Guid>>())).Verifiable();
            _database.Setup(d => d.UpdateOnSubmit(It.IsAny<IEntity<TestEntity, Guid>>())).Verifiable();

            _entities = new List<TestEntity> {
                new TestEntity(),
                new TestEntity(),
                new TestEntity(),
                new TestEntity()
            };
            _database
                .Setup(d => d.GetSource<TestEntity, Guid>())
                .Returns(_entities.AsQueryable)
                .Verifiable();
            
            _repository = new BaseRepositoryImpl(_database.Object);
        }

        [Test]
        public void Constructor_should_throw_exception_if_argument_null()
        {
            Assert.Throws<ArgumentNullException>(() => new BaseRepositoryImpl((IDatabase) null));
            Assert.Throws<ArgumentNullException>(() => new BaseRepositoryImpl((IDatabaseFactory)null));
        }

        [Test]
        public void Add_should_call_IDatabase_AddOnSubmit()
        {
            var entity = new TestEntity();
            _repository.Add(entity);
            _database.Verify(d => d.AddOnSubmit(entity));
        }

        [Test]
        public void Remove_should_call_IDatabase_DeleteOnSubmit()
        {
            var entity = new TestEntity();
            _repository.Remove(entity);
            _database.Verify(d => d.DeleteOnSubmit(entity));
        }

        [Test]
        public void Update_should_call_IDatabase_UpdateOnSubmit()
        {
            var entity = new TestEntity();
            _repository.Update(entity);
            _database.Verify(d => d.UpdateOnSubmit(entity));
        }

        [Test]
        public void GetById_should_return_correct_result()
        {
            var entity = _entities[0];
            var repoEntity = _repository.GetById(entity.Id);
            Assert.AreSame(entity, repoEntity);
            _database.Verify(d => d.GetSource<TestEntity, Guid>());
        }

        [Test]
        public void GetAll_should_return_correct_result()
        {
            
            var repoEntities = _repository.GetAll();
            CollectionAssert.AreEquivalent(repoEntities, _entities);
            _database.Verify(d => d.GetSource<TestEntity, Guid>());
        }
    }
}