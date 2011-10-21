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
            var user = new User("Name", "Password", false);
            using (var tran = Session.BeginTransaction())
            {
                Session.Save(user);

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
                Assert.True(loadedUser.IsPasswordEqual("Password"));

                tran.Commit();
            }

        }
    }
}