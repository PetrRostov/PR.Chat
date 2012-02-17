using System;
using System.Linq;
using System.Reflection;
using NHibernate.Linq;
using NUnit.Framework;
using PR.Chat.Domain;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Data.NH.Tests.Mapping
{
    [TestFixture, Category(TestCategory.DataMapping)]
    public class MembershipMappingFixtures : BaseMappingFixtures
    {
        public MembershipMappingFixtures()
            : base(typeof(PerSessionNHibernateDatabase).Assembly)
        {
        }

        [Test]
        public void Save_and_load_should_work()
        {
            var password        = "password";
            var registerDate    = DateTime.Now;
            registerDate        = new DateTime(
                registerDate.Year,
                registerDate.Month,
                registerDate.Day,
                registerDate.Hour,
                registerDate.Minute,
                registerDate.Second
            ); 
            var lastLogin       = registerDate.AddHours(1);
            var user            = new User(false);
            var membership      = new Membership(user, "@@@", password, registerDate);

            using (var tran = Session.BeginTransaction())
            {
                Session.Save(user);
                Session.Save(membership);

                tran.Commit();
            }

            Session.Clear();

            using (var tran = Session.BeginTransaction())
            {
                var loadedMembership = Session.Query<Membership>()
                    .Where(m => m.Login == membership.Login)
                    .First();

                Assert.AreEqual(membership.Login, loadedMembership.Login);
                Assert.IsTrue(membership.User.SameIdentityAs(user));
                Assert.IsTrue(membership.IsPasswordEqual(password));
                Assert.AreEqual(membership.Id, loadedMembership.Id);
                Assert.IsTrue(membership.SameIdentityAs(loadedMembership));
                Assert.IsNull(membership.LastLogin);
                Assert.AreEqual(membership.RegisteredAt, loadedMembership.RegisteredAt);
                
                loadedMembership.LastLogin = lastLogin;

                tran.Commit();
            }


            Session.Clear();

            using (var tran = Session.BeginTransaction())
            {
                var loadedMembership = Session.Query<Membership>()
                    .Where(m => m.Login == membership.Login)
                    .First();

                Assert.AreEqual(lastLogin, loadedMembership.LastLogin);

                tran.Commit();
            }
        }
    }
}