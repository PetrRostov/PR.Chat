using NUnit.Framework;
using PR.Chat.Test.Common;

namespace PR.Chat.Domain.Tests
{
    [TestFixture, Category(TestCategory.Domain)]
    public class UserFatoryFixtures
    {
        private UserFactory _userFactory;

        [TestFixtureSetUp]
        public void ClassInit()
        {
            _userFactory = new UserFactory();
        }

        [Test]
        public void CreateUregistered_should_create_unregistered_users()
        {
            User user = _userFactory.CreateUnregistered();
            Assert.IsFalse(user.IsRegistered);
        }

        [Test]
        public void CreateUregistered_should_create_different_users()
        {
            User user1 = _userFactory.CreateUnregistered();
            User user2 = _userFactory.CreateUnregistered();
            
            Assert.IsFalse(user1.SameIdentityAs(user2));
        }

        public void CreateRegistered_should_create_user_with_correct_fields()
        {
            const string name = "name1";
            const string password = "password2";

            User user = _userFactory.CreateRegistered(name, password);
            Assert.AreEqual(user.Name, name);
            Assert.IsTrue(user.IsPasswordEqual(password));
            Assert.IsTrue(user.IsRegistered);
        }
    }


}