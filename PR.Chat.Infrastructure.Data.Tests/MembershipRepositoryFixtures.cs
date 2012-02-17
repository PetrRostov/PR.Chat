using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using PR.Chat.Domain;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Data.Tests
{
    [TestFixture, Category(TestCategory.Infrastructure)]
    public class MembershipRepositoryFixtures
    {
        private IQueryable<Membership> _membershipSource;
        private Mock<IDatabase> _database;
        private MembershipRepository _membershipRepository;
        private Mock<User> _user;

        [SetUp]
        public void Init()
        {
            var userId = Guid.NewGuid();
            _user = new Mock<User>();
            _user.Setup(u => u.Id).Returns(userId);

            var user = _user.Object;// new User(false);
            _membershipSource = new[] {
                new Membership(user, "login1", "password2", DateTime.UtcNow),
                new Membership(user, "loGin2", "password2", DateTime.UtcNow),
                new Membership(user, "LOgin4", "passwor32", DateTime.UtcNow)
            }.AsQueryable();
            

            _database = new Mock<IDatabase>();
            _database.Setup(d => d.GetSource<Membership, Guid>()).Returns(_membershipSource);

            _membershipRepository = new MembershipRepository(_database.Object);
        }

        [Test]
        public void GetByLogin_should_return_right_result()
        {
            var membership = _membershipRepository.GetByLogin("loGin1");

            Assert.IsNotNull(membership);
            Assert.IsTrue(membership.Login.Equals("login1", StringComparison.InvariantCultureIgnoreCase));

            membership = _membershipRepository.GetByLogin("loGin2");
            Assert.IsNotNull(membership);
            Assert.IsTrue(membership.Login.Equals("login2", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void GetByLogin_should_throw_exception_if_entity_not_found()
        {
            Assert.Throws<EntityNotFoundException>(() => _membershipRepository.GetByLogin("loGin12"));
        }



        [Test]
        public void GetByUser_should_return_empty_collection_if_user_not_match()
        {
            var userId = Guid.NewGuid();
            var user = new Mock<User>();
            user.Setup(u => u.Id).Returns(userId);

            CollectionAssert.IsEmpty(_membershipRepository.GetByUser(user.Object));

        }

        [Test]
        public void GetByUser_should_return_right_result()
        {
            var memberships = _membershipRepository.GetByUser(_user.Object);

            Assert.AreEqual(memberships.Count(), 3);
        }

        [Test]
        public void ExistsWithLogin_should_return_right_result()
        {
            Assert.IsTrue(_membershipRepository.ExistsWithLogin("lOgIn1"));
            Assert.IsFalse(_membershipRepository.ExistsWithLogin("lOgIn145")); 
        }
    }
}