using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PR.Chat.Test.Common;

namespace PR.Chat.Domain.Tests
{
    [TestFixture, Category(TestCategory.Domain)]
    class NickFixtures
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
            Assert.AreNotEqual(nick.Id, Guid.Empty);
        }

        [Test]
        public void IDs_should_be_not_equals_if_used_constructor()
        {
            var nick1 = new Nick(_user, "name");
            var nick2 = new Nick(_user, "name");

            Assert.AreNotEqual(nick1.Id, nick2.Id);
        }

        [Test]
        public void SameIdentityAs_should_return_true_for_equals_Names()
        {
            var nick1 = new Nick(_user, "name");
            var nick2 = new Nick(_user, "NamE");

            Assert.IsTrue(nick1.SameIdentityAs(nick2));
            Assert.IsTrue(nick2.SameIdentityAs(nick1));
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
