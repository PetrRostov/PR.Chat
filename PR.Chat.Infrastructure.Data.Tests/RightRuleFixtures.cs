using System;
using System.Linq.Expressions;
using Moq;
using NUnit.Framework;
using PR.Chat.Domain;
using PR.Chat.Infrastructure.RightContext;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Data.Tests
{
    [TestFixture, Category(TestCategory.Infrastructure)]
    public class RightRuleFixtures
    {
        private Mock<IPermission> _permission;

        [SetUp]
        public void Init()
        {
            _permission = new Mock<IPermission>();
            _permission
                .SetupGet(p => p.CheckExpressionType)
                .Returns(typeof(Expression<Func<Nick, Room, bool>>));
        }

        [Test, Ignore]
        public void Constructor_should_throw_exception_if_checkExpression_is_invalid()
        {
            Assert.Throws<ArgumentException>(
                () => new RightRule(
                    Guid.NewGuid(),
                    Guid.NewGuid(), 
                    "PermissionName",
                    (Expression<Func<User, string, bool>>)((user, room) => false),
                    DateTime.Now
                )
            );
        }

        [Test, Ignore]
        public void Constructor_should_work_if_checkExpression_is_valid()
        {
            var rightRule = new RightRule(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "PermissionName",
                (Expression<Func<Nick, Room, bool>>)((nick, room) => nick.Name == "Opa"),
                DateTime.Now
            );
        }

    }
}