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

        public void CreateRegistered_should_create_user_with_correct_fields()
        {
            User user = _userFactory.CreateRegistered();
            Assert.IsTrue(user.IsRegistered);
        }
    }


}