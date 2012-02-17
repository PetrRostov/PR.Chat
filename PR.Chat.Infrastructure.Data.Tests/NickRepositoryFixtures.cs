using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using PR.Chat.Domain;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Data.Tests
{
    [TestFixture, Category(TestCategory.Infrastructure)]
    public class NickRepositoryFixtures
    {
        private IQueryable<Nick> _nickSource;
        private Mock<IDatabase> _database;
        private NickRepository _nickRepository;

        [SetUp]
        public void Init()
        {
            var user = new User(false);


            _nickSource = new[] {
                new Nick(user, "login1"),
                new Nick(user, "loGin2"),
                new Nick(user, "LOgin4")
            }.AsQueryable();


            _database = new Mock<IDatabase>();
            _database.Setup(d => d.GetSource<Nick, Guid>()).Returns(_nickSource);

            _nickRepository = new NickRepository(_database.Object);
        }

        [Test]
        public void GetByName_should_return_right_result()
        {
            var nick = _nickRepository.GetByName("loGin1");

            Assert.IsNotNull(nick);
            Assert.IsTrue(nick.Name.Equals("login1", StringComparison.InvariantCultureIgnoreCase));

            nick = _nickRepository.GetByName("loGin2");
            Assert.IsNotNull(nick);
            Assert.IsTrue(nick.Name.Equals("login2", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void GetByName_should_throw_exception_if_entity_not_found()
        {
            Assert.Throws<EntityNotFoundException>(() => _nickRepository.GetByName("sfdf"));
        }

        [Test]
        public void ExistsWithName_should_return_correnct_result()
        {
            Assert.IsTrue(_nickRepository.ExistsWithName("lOgIn1"));
            Assert.IsFalse(_nickRepository.ExistsWithName("lOgIn1gg"));
        }
    }
}