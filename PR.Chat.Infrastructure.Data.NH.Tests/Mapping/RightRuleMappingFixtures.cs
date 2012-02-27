using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;
using NHibernate.Linq;
using PR.Chat.Domain;
using PR.Chat.Infrastructure.RightContext;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Data.NH.Tests.Mapping
{
    [TestFixture, Category(TestCategory.DataMapping)]
    public class RightRuleMappingFixtures : BaseMappingFixtures
    {
        public RightRuleMappingFixtures()
            : base(typeof(PerSessionNHibernateDatabase).Assembly)
        {


        }

        [Test]
        public void Save_and_load_should_work()
        {
            var ownerId = Guid.NewGuid();
            var permission = "OpaPermission";
            var expression = (Expression<Func<Nick, Room, bool>>) ((nick, room) => true);
            var expiredAt = DateTime.Now.AddYears(1).Date;

            var rightRule = new RightRule(
                Guid.Empty,
                ownerId,
                permission,
                expression, 
                expiredAt
            );
            Guid id;

            using (var tran = Session.BeginTransaction())
            {
                id = (Guid)Session.Save(rightRule);

                tran.Commit();
            }
            Session.Clear();
            
            using (var tran = Session.BeginTransaction())
            {
                rightRule = Session
                    .Query<RightRule>().Where(rr => rr.OwnerId == ownerId && rr.Permission.ToLowerInvariant() == permission.ToLowerInvariant())
                    .First();

                Assert.NotNull(rightRule);
                Assert.AreEqual(rightRule.OwnerId, ownerId);
                Assert.AreEqual(rightRule.Permission, permission);
                Assert.AreEqual(rightRule.ExpiredAt, expiredAt);
                

                tran.Commit();
            }
            Session.Clear();


        }
    }
}