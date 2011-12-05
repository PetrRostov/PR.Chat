using System;
using NUnit.Framework;
using PR.Chat.Test.Common;

namespace PR.Chat.Domain.Tests
{
    [TestFixture, Category(TestCategory.Domain)]
    public class MembershipFactoryFixtures
    {
        private IMembershipFactory _membershipFactory;

        [TestFixtureSetUp]
        public void Init()
        {
            _membershipFactory = new MembershipFactory();
        }

        [Test]
        public void Create_should_return_correct_object()
        {
            var user        = new User(true);
            var login       = "login1";
            var password    = "password2";
            var registerAt  = DateTime.UtcNow;


            var membership = _membershipFactory.Create(
                user,
                login,
                password,
                registerAt
            );

            Assert.IsTrue(membership.User.SameIdentityAs(user));
            Assert.AreEqual(login, membership.Login);
            Assert.IsTrue(membership.IsPasswordEqual(password));
            Assert.AreEqual(membership.RegisteredAt, registerAt);

        }
    }
}