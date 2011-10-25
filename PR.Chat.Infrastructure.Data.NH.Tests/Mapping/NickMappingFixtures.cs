using System.Linq;
using System.Reflection;
using NHibernate.Linq;
using NUnit.Framework;
using PR.Chat.Domain;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Data.NH.Tests.Mapping
{
    [TestFixture, Category(TestCategory.DataMapping)]
    public class NickMappingFixtures : BaseMappingFixtures
    {
        public NickMappingFixtures()
            : base(typeof(NHibernateDatabase).Assembly)
        {
        }

        [Test]
        public void Save_and_load_should_work()
        {
            var user = new User("Name", "Password", false);
            var nick = new Nick(user, "Nick");
            using (var tran = Session.BeginTransaction())
            {
                Session.Save(user);
                Session.Save(nick);

                tran.Commit();
            }

            Session.Clear();

            using (var tran = Session.BeginTransaction())
            {
                var loadedNick = Session.Query<Nick>().Where(n => n.Name == nick.Name).First();

                Assert.AreEqual(nick.Name, loadedNick.Name);
                Assert.IsTrue(nick.User.SameIdentityAs(user));
                Assert.AreEqual(nick.Id, loadedNick.Id);
                Assert.IsTrue(nick.SameIdentityAs(loadedNick));

                tran.Commit();
            }

        }
    }
}