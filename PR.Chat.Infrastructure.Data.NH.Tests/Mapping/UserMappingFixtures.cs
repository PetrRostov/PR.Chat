using System;
using System.Linq;
using NUnit.Framework;
using PR.Chat.Domain;
using NHibernate.Linq;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Data.NH.Tests.Mapping
{
    [TestFixture, Category(TestCategory.DataMapping)]
    public class UserMappingFixtures : BaseMappingFixtures
    {
        public UserMappingFixtures() 
            : base(typeof(NHibernateDatabase).Assembly)
        {
        }

        [Test]
        public void Save_and_load_should_work()
        {
            var password = Guid.NewGuid().ToString();
            var user = new User(Guid.NewGuid().ToString(), password, false);

            var nick1 = new Nick(user, Guid.NewGuid().ToString());
            var nick2 = new Nick(user, Guid.NewGuid().ToString()); 

            using (var tran = Session.BeginTransaction())
            {
                Session.Save(user);
                Session.Save(nick1);
                Session.Save(nick2);

                tran.Commit();
            }

            Session.Clear();

            using (var tran = Session.BeginTransaction())
            {
                var loadedUser = Session.Query<User>().Where(u => u.Name == user.Name).First();

                Assert.AreEqual(user.Name, loadedUser.Name);
                Assert.AreEqual(user.IsRegistered, loadedUser.IsRegistered);
                Assert.AreEqual(user.Id, loadedUser.Id);
                Assert.True(user.SameIdentityAs(loadedUser));
                Assert.True(loadedUser.IsPasswordEqual(password));

                Assert.AreEqual(loadedUser.Nicks.Count(), 2);
                Assert.IsTrue(loadedUser.Nicks.Any(n => n.Name == nick1.Name));
                Assert.IsTrue(loadedUser.Nicks.Any(n => n.Name == nick2.Name));


                Session.Delete(loadedUser);
                tran.Commit();
            }

        }
    }
}