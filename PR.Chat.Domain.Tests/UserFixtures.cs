using System;
using PR.Chat.Domain;
using NUnit.Framework;

namespace PR.Chat.Domain.Tests
{
    /// <summary>
    /// Summary description for UserFixtures
    /// </summary>
    [TestFixture, Category("Domain objects")]
    public class UserFixtures
    {
        [Test]
        public void Constructor_should_work()
        {
            const string name = "Name 123";
            var user = new User(name, "Password");
            Assert.AreEqual(user.Name, name);
            Assert.AreNotEqual(Guid.Empty, user.Id);
        }
        
        [Test]
        public void Constructor_should_throw_exception_if_argument_null_or_empty()
        {
            Assert.Throws<ArgumentException>(() => new User(null, "1"));
            Assert.Throws<ArgumentException>(() => new User("1", null));
            Assert.Throws<ArgumentException>(() => new User("", "123"));
            Assert.Throws<ArgumentException>(() => new User("123", ""));
        }


        [Test]
        [TestCase("4")]
        [TestCase("Pass")]
        [TestCase("KLfd;kfdh")]
        public void IsPasswordEqual_should_work(string password)
        {
            var user = new User("Name", password);
            Assert.IsTrue(user.IsPasswordEqual(password));
            Assert.IsFalse(user.IsPasswordEqual("123"));
        }

        [Test]
        public void SameIdentityAs_should_work()
        {
            var user1 = new User("userName", "opa");
            var user2 = new User("UserName", "opa3");
            var user3 = new User("NickName213", "opa3");

            Assert.IsTrue(user1.SameIdentityAs(user2));
            Assert.IsTrue(user2.SameIdentityAs(user1));
            Assert.IsFalse(user3.SameIdentityAs(user1));
            Assert.IsFalse(user3.SameIdentityAs(user2));
            Assert.IsFalse(user2.SameIdentityAs(user3));
        }

        [Test]
        public void Equals_should_work()
        {
            var user1 = new User("UseRName", "opa");
            var user2 = new User("UserName", "op34a");
            var user3 = new User("Nick4Name", "op34a");

            Assert.IsTrue(user1.Equals(user2));
            Assert.IsFalse(user1.Equals(user3));
            Assert.IsFalse(user1.Equals(null));
            Assert.IsFalse(user1.Equals(4));
        }

        [Test]
        public void GetHashCode_should_work()
        {
            var user1 = new User("UserName", "opa");
            var user2 = new User("UserNamE", "op34a");
            var user3 = new User("Nick4Name", "op34a");

            Assert.AreEqual(user1.GetHashCode(), user2.GetHashCode());
            Assert.AreNotEqual(user1.GetHashCode(), user3.GetHashCode());
        }

        [Test]
        public void CreateNick_should_work()
        {
            var nickName = "NickName";
            var user = new User("UserName", "opa");
            Nick nick = user.CreateNick("NickName");

            Assert.AreEqual(nick.Name, nickName);
        }
    }
}