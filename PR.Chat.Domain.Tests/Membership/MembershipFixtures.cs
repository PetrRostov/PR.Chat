using NUnit.Framework;

namespace PR.Chat.Domain.Tests.Membership
{
    [TestFixture]
    public class MembershipFixtures
    {

        private User _user;

        [SetUp]
        public void Init()
        {
            _user = new User("Name", "Password", false);
        }

        [Test]
        public void Constructor_Should_Work()
        {
            const string nickName = @"Name";
            var nick = new Nick(_user, nickName);
            Assert.AreEqual(nick.Name, nickName);
        }

        [Test]
        public void SameIdentityAs_should_return_false_for_not_equals_Names()
        {
            var nick1 = new Nick(_user, "name1");
            var nick2 = new Nick(_user, "name");

            Assert.IsFalse(nick1.SameIdentityAs(nick2));
            Assert.IsFalse(nick1.SameIdentityAs(null));
        }

        [Test]
        public void GetHashcode_should_return_equals_values_for_equals_Names()
        {
            var nick1 = new Nick(_user, "Name");
            var nick2 = new Nick(_user, "NamE");

            Assert.AreEqual(nick1.GetHashCode(), nick2.GetHashCode());
        }

        [Test]
        public void GetHashcode_should_return_not_equals_values_for_not_equals_Names()
        {
            var nick1 = new Nick(_user, "Opa");
            var nick2 = new Nick(_user, "Opa1");
            Assert.AreNotEqual(nick2.GetHashCode(), nick1.GetHashCode());
        }

        [Test]
        public void Equals_should_return_true_for_equals_Names()
        {
            var nick1 = new Nick(_user, "Opa");
            var nick2 = new Nick(_user, "OpA");
            Assert.IsTrue(nick1.Equals(nick2));
        }

        [Test]
        public void Equals_should_return_false_for_not_equals_Names()
        {
            var nick1 = new Nick(_user, "Opa");
            var nick2 = new Nick(_user, "OpA1");
            Assert.IsFalse(nick1.Equals(nick2));
            Assert.IsFalse(nick1.Equals(1));
            Assert.IsFalse(nick1.Equals(null));
        }
    }
}