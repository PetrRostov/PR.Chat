using System;
using PR.Chat.Domain;
using NUnit.Framework;
using PR.Chat.Test.Common;

namespace PR.Chat.Domain.Tests
{
    /// <summary>
    /// Summary description for UserFixtures
    /// </summary>
    [TestFixture, Category(TestCategory.Domain)]
    public class UserFixtures
    {
        [Test]
        public void Constructor_should_work()
        {
            const string name = "Name 123";
            var user = new User(name, "Password", false);
            Assert.AreEqual(user.Name, name);
            Assert.AreNotEqual(Guid.Empty, user.Id);
            Assert.IsFalse(user.IsRegistered);
        }
        
        [Test]
        public void Constructor_should_throw_exception_if_argument_null_or_empty()
        {
            Assert.Throws<ArgumentNullException>(() => new User(null, "1", false));
            Assert.Throws<ArgumentNullException>(() => new User("1", null, false));
            Assert.Throws<ArgumentException>(() => new User("", "123", false));
            Assert.Throws<ArgumentException>(() => new User("123", "", false));
        }


        [Test]
        [TestCase("4")]
        [TestCase("Pass")]
        [TestCase("KLfd;kfdh")]
        public void IsPasswordEqual_should_work(string password)
        {
            var user = new User("Name", password, false);
            Assert.IsTrue(user.IsPasswordEqual(password));
            Assert.IsFalse(user.IsPasswordEqual("123"));
        }

        [Test]
        public void SameIdentityAs_should_work()
        {
            var user1 = new User("userName", "opa", false);
            var user2 = new User("UserName", "opa3", false);
            var user3 = new User("NickName213", "opa3", false);

            Assert.IsTrue(user1.SameIdentityAs(user2));
            Assert.IsTrue(user2.SameIdentityAs(user1));
            Assert.IsFalse(user3.SameIdentityAs(user1));
            Assert.IsFalse(user3.SameIdentityAs(user2));
            Assert.IsFalse(user2.SameIdentityAs(user3));
        }

        [Test]
        public void Equals_should_work()
        {
            var user1 = new User("UseRName", "opa", false);
            var user2 = new User("UserName", "op34a", false);
            var user3 = new User("Nick4Name", "op34a", false);

            Assert.IsTrue(user1.Equals(user2));
            Assert.IsFalse(user1.Equals(user3));
            Assert.IsFalse(user1.Equals(null));
            Assert.IsFalse(user1.Equals(4));
        }

        [Test]
        public void GetHashCode_should_work()
        {
            var user1 = new User("UserName", "opa", false);
            var user2 = new User("UserNamE", "op34a", false);
            var user3 = new User("Nick4Name", "op34a", false);

            Assert.AreEqual(user1.GetHashCode(), user2.GetHashCode());
            Assert.AreNotEqual(user1.GetHashCode(), user3.GetHashCode());
        }

        [Test]
        public void CreateNick_should_work()
        {
            var nickName = "NickName";
            var user = new User("UserName", "opa", false);
            Nick nick = user.CreateNick("NickName");

            Assert.AreEqual(nick.Name, nickName);
        }

        [Test]
        public void CreateNick_should_throws_exception_if_name_is_null_or_empty()
        {
            var user = new User("UserName", "opa", false);
            Assert.Throws<ArgumentNullException>(() => user.CreateNick(null));
            Assert.Throws<ArgumentException>(() => user.CreateNick(string.Empty));
        }

        [Test]
        public void SetPassword_should_throws_exception_if_password_is_null_or_empty()
        {
            var user = new User("UserName", "opa", false);
            Assert.Throws<ArgumentNullException>(() => user.SetPassword(null));
            Assert.Throws<ArgumentException>(() => user.SetPassword(string.Empty));
        }

        [Test]
        public void SetPassword_should_change_password()
        {
            var user = new User("UserName", "opa", false);
            const string newPassword = "op1ddd";
            user.SetPassword(newPassword);
            Assert.IsTrue(user.IsPasswordEqual(newPassword));
        }
    }
}