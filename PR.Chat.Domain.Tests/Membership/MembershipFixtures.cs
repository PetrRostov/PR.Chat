using System;
using NUnit.Framework;
using PR.Chat.Test.Common;

namespace PR.Chat.Domain.Tests
{
    [TestFixture, Category(TestCategory.Domain)]
    public class MembershipFixtures
    {

        private User _user;

        [SetUp]
        public void Init()
        {
            _user = new User(false);
        }

        [Test]
        public void Constructor_Should_Work()
        {
            const string login = @"Name";
            const string pass = @"pass";
            var registeredAt = DateTime.UtcNow;

            var membership = new Membership(_user, login, pass, registeredAt);
            Assert.AreEqual(membership.User, _user);
            Assert.IsTrue(membership.User.SameIdentityAs(_user));

            Assert.AreEqual(membership.Login, login);
            Assert.IsTrue(membership.IsPasswordEqual(pass));

            Assert.AreEqual(membership.RegisteredAt, registeredAt);
        }

        [Test]
        public void Constructor_should_throw_exception_if_argument_null_or_empty()
        {
            Assert.Throws<ArgumentNullException>(() => new Membership(null, "1", "1", DateTime.UtcNow));
            Assert.Throws<ArgumentNullException>(() => new Membership(_user, null, "1", DateTime.UtcNow));
            Assert.Throws<ArgumentNullException>(() => new Membership(_user, "1", null, DateTime.UtcNow));

            Assert.Throws<ArgumentException>(() => new Membership(_user, string.Empty, "1", DateTime.UtcNow));
            Assert.Throws<ArgumentException>(() => new Membership(_user, "1", string.Empty, DateTime.UtcNow));
        }


        [Test]
        [TestCase("4")]
        [TestCase("Pass")]
        [TestCase("KLfd;kfdh")]
        public void IsPasswordEqual_should_work(string password)
        {
            var user = new Membership(_user, "login", password, DateTime.UtcNow);
            Assert.IsTrue(user.IsPasswordEqual(password));
            Assert.IsFalse(user.IsPasswordEqual("123"));
        }

        [Test]
        public void SameIdentityAs_should_return_false_for_not_equals_Names()
        {
            var membership1 = new Membership(_user, "name1", "pass", DateTime.UtcNow);
            var membership2 = new Membership(_user, "name2", "pass", DateTime.UtcNow);

            Assert.IsFalse(membership1.SameIdentityAs(membership2));
            Assert.IsFalse(membership1.SameIdentityAs(null));
        }

        [Test]
        public void GetHashcode_should_return_equals_values_for_equals_Names()
        {
            var membership1 = new Membership(_user, "name1", "pass", DateTime.UtcNow);
            var membership2 = new Membership(_user, "name1", "pass", DateTime.UtcNow);

            Assert.AreEqual(membership1.GetHashCode(), membership2.GetHashCode());
        }

        [Test]
        public void GetHashcode_should_return_not_equals_values_for_not_equals_Names()
        {
            var membership1 = new Membership(_user, "name1", "pass", DateTime.UtcNow);
            var membership2 = new Membership(_user, "name2", "pass", DateTime.UtcNow);

            Assert.AreNotEqual(membership1.GetHashCode(), membership2.GetHashCode());
        }

        [Test]
        public void Equals_should_return_true_for_equals_Names()
        {
            var membership1 = new Membership(_user, "name1", "pass", DateTime.UtcNow);
            var membership2 = new Membership(_user, "NaMe1", "pass", DateTime.UtcNow);

            Assert.IsTrue(membership1.Equals(membership2));
        }

        [Test]
        public void Equals_should_return_false_for_not_equals_Names()
        {
            var membership1 = new Membership(_user, "name1", "pass", DateTime.UtcNow);
            var membership2 = new Membership(_user, "name2", "pass", DateTime.UtcNow);

            Assert.IsFalse(membership1.Equals(membership2));
            Assert.IsFalse(membership1.Equals(1));
            Assert.IsFalse(membership1.Equals(null));
        }

    }
}