using System;
using System.Collections.ObjectModel;
using System.Linq;
using Moq;
using NUnit.Framework;
using PR.Chat.Application.DTO;
using PR.Chat.Domain;
using PR.Chat.Test.Common;

namespace PR.Chat.Application.Tests
{
    [TestFixture, Category(TestCategory.DtoMapping)]
    public class MappingFixtures
    {
        private MappingProvider mappingProvider;

        [SetUp]
        public void Init()
        {
             mappingProvider = new MappingProvider();
        }

        [Test]
        public void GetMapping_should_throw_exception_if_mapping_not_exists()
        {
            Assert.Throws<MappingNotExistsException>(() => mappingProvider.GetMapping<string, Guid>());
        }

        [Test]
        public void GetMapping_should_work()
        {
            mappingProvider.GetMapping<User, UserDto>();
        }

        [Test]
        public void User_to_UserDto_should_work()
        {
            var id = Guid.NewGuid();
            var user = new Mock<User>();
            var nicks = new Collection<Nick> {
                new Nick(user.Object, "Name1"),
                new Nick(user.Object, "Name2"),
                new Nick(user.Object, "Name3")
            };

            user.SetupGet(u => u.Id).Returns(id);
            user.SetupGet(u => u.Nicks).Returns(nicks);
            user.SetupGet(u => u.IsRegistered).Returns(false);

            var userMapping = mappingProvider.GetMapping<User, UserDto>();

            var userDto = userMapping.Convert(user.Object);

            Assert.IsNotNull(userDto);
            Assert.AreEqual(Guid.Parse(userDto.Id), id);
            Assert.IsNotNull(userDto.Nicks);
            Assert.AreEqual(userDto.Nicks.Count(), 3);
            CollectionAssert.AllItemsAreInstancesOfType(userDto.Nicks, typeof(NickDto));
        }

        [Test]
        public void Nick_to_NickDto_should_work()
        {
            var id      = Guid.NewGuid();
            var nick    = new Mock<Nick>();
            nick.SetupGet(n => n.Name).Returns("NickName");
            nick.SetupGet(n => n.Id).Returns(id);

            var nickMapping = mappingProvider.GetMapping<Nick, NickDto>();

            var nickDto = nickMapping.Convert(nick.Object);

            Assert.IsNotNull(nickDto);
            Assert.AreEqual(Guid.Parse(nickDto.Id), id);
            Assert.AreEqual(nickDto.Name, "NickName");
        }

        [Test]
        public void Membership_to_MembershipDto_should_work()
        {
            var userId = Guid.NewGuid();
            var user = new Mock<User>();
            var nicks = new Collection<Nick> {
                new Nick(user.Object, "Name1"),
                new Nick(user.Object, "Name2"),
                new Nick(user.Object, "Name3")
            };
            user.SetupGet(u => u.Id).Returns(userId);
            user.SetupGet(u => u.Nicks).Returns(nicks);
            user.SetupGet(u => u.IsRegistered).Returns(false);

            var membershipId = Guid.NewGuid();
            var membership = new Mock<Membership>();
            membership.SetupGet(m => m.Login).Returns("Login");
            membership.SetupGet(m => m.Id).Returns(membershipId);
            membership.SetupGet(m => m.User).Returns(user.Object);

            var membershipMapping = mappingProvider.GetMapping<Membership, MembershipDto>();
            var membershipDto = membershipMapping.Convert(membership.Object);

            Assert.IsNotNull(membershipDto);
            Assert.AreEqual(membershipDto.Login, "Login");
            Assert.IsNotNull(membershipDto.User);
        }
    }
}