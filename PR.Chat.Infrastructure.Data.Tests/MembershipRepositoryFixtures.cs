﻿using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using PR.Chat.Domain;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Data.Tests
{
    [TestFixture, Category(TestCategory.Infrastructure)]
    public class MembershipRepositoryFixtures
    {
        private IQueryable<Membership> _membershipSource;
        private Mock<IDatabase> _database;
        private MembershipRepository _membershipRepository;

        [SetUp]
        public void Init()
        {
            var user = new User(false);
            _membershipSource = new[] {
                new Membership(user, "login1", "password2", DateTime.UtcNow),
                new Membership(user, "loGin2", "password2", DateTime.UtcNow),
                new Membership(user, "LOgin4", "passwor32", DateTime.UtcNow)
            }.AsQueryable();
            

            _database = new Mock<IDatabase>();
            _database.Setup(d => d.GetSource<Membership, Guid>()).Returns(_membershipSource);

            _membershipRepository = new MembershipRepository(_database.Object);
        }

        [Test]
        public void GetByLogin_should_return_right_result()
        {
            var membership = _membershipRepository.GetByLogin("loGin1");

            Assert.IsNotNull(membership);
            Assert.IsTrue(membership.Login.Equals("login1", StringComparison.InvariantCultureIgnoreCase));

            membership = _membershipRepository.GetByLogin("loGin2");
            Assert.IsNotNull(membership);
            Assert.IsTrue(membership.Login.Equals("login2", StringComparison.InvariantCultureIgnoreCase));
        }

    }
}