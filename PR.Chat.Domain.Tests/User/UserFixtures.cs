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
            var user = new User(false);
            Assert.IsFalse(user.IsRegistered);
        }
        

        [Test]
        public void SameIdentityAs_should_work()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();

            var user1 = new UserWithSetters();
            user1.SetId(id1);

            var user2 = new UserWithSetters();
            user2.SetId(id1);

            var user3 = new UserWithSetters();
            user3.SetId(id2);

            Assert.IsTrue(user1.SameIdentityAs(user2));
            Assert.IsTrue(user2.SameIdentityAs(user1));
            Assert.IsFalse(user3.SameIdentityAs(user1));
            Assert.IsFalse(user3.SameIdentityAs(user2));
            Assert.IsFalse(user2.SameIdentityAs(user3));
        }


        [Test]
        public void NewUsers_should_be_equals()
        {
            var user1 = new User(false);
            var user2 = new User(true);
            var user3 = new User(false);

            Assert.IsTrue(user1.Equals(user2));
            Assert.IsTrue(user2.Equals(user3));
            Assert.IsTrue(user3.Equals(user1)); 
        }

        [Test]
        public void GetHashCode_should_work()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();

            var user1 = new UserWithSetters();
            user1.SetId(id1);

            var user2 = new UserWithSetters();
            user2.SetId(id1);

            var user3 = new UserWithSetters();
            user3.SetId(id2);

            Assert.AreEqual(user1.GetHashCode(), user2.GetHashCode());
            Assert.AreNotEqual(user3.GetHashCode(), user2.GetHashCode());
        }

        [Test]
        public void CreateNick_should_work()
        {
            var nickName = "NickName";
            var user = new User(false);
            Nick nick = user.CreateNick("NickName");

            CollectionAssert.Contains(user.Nicks, nick);

            Assert.AreEqual(nick.Name, nickName);
        }

        [Test]
        public void SetAsRegistered_should_work()
        {
            var user = new User(false);
            user.SetAsRegistered();

            Assert.IsTrue(user.IsRegistered);
        }

        [Test]
        public void CreateNick_should_throws_exception_if_name_is_null_or_empty()
        {
            var user = new User(false);
            Assert.Throws<ArgumentNullException>(() => user.CreateNick(null));
            Assert.Throws<ArgumentException>(() => user.CreateNick(string.Empty));
        }
    }
}