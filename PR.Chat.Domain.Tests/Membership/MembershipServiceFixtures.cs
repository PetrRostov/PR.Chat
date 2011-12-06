using System;
using Moq;
using NUnit.Framework;
using PR.Chat.Test.Common;

namespace PR.Chat.Domain.Tests
{
    [TestFixture, Category(TestCategory.Domain)]
    public class MembershipServiceFixtures
    {
        private User _user;
        private User _userRegistered;
        private MembershipService _membershipService;
        private Mock<IMembershipRepository> _membershipRepository;
        private Mock<IMembershipFactory> _membershipFactory;
        private Mock<IUserRepository> _userRepository;
        private Mock<IUserFactory> _userFactory;

            
        [TestFixtureSetUp]
        public void Init()
        {
            _user = new User(false);
            _userRegistered = new User(true);
            
            _membershipRepository = new Mock<IMembershipRepository>();
            _membershipRepository
                .Setup(r => r.Add(It.IsAny<Membership>()))
                .Verifiable();

            _membershipFactory = new Mock<IMembershipFactory>();
            _membershipFactory
                .Setup(mf => mf.Create(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns<User, string, string, DateTime>(
                    (user, login, password, registeredAt) => new Membership(user, login, password, registeredAt)
                )
                .Verifiable();

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(r => r.Add(It.IsAny<User>())).Verifiable();

            _userFactory = new Mock<IUserFactory>();
            _userFactory
                .Setup(uf => uf.CreateRegistered())
                .Returns(_userRegistered)
                .Verifiable();



            _membershipService = new MembershipService(
                _membershipRepository.Object,
                _membershipFactory.Object,
                _userRepository.Object,
                _userFactory.Object
            );
            

        }

        [Test]
        public void RegisterWithUser_set_this_user_as_membership_owner()
        {
            var membership = _membershipService.Register(_user, "Login", "Password");
            Assert.AreSame(membership.User, _user);
        }

        [Test]
        public void Register_should_add_new_membership_to_repository()
        {
            var membership = _membershipService.Register("login", "pass");
            _membershipRepository.Verify(r => r.Add(membership));
        }

        [Test]
        public void Register_should_add_new_user_to_repository()
        {
            _membershipService.Register("login", "pass");
            _userRepository.Verify(r => r.Add(_userRegistered));
        }


        [Test]
        public void Register_should_use_factory_for_creating_user()
        {
            _membershipService.Register("login", "pass");
            _userFactory.Verify(uf => uf.CreateRegistered());
        }

        [Test]
        public void Register_should_use_factory_for_creating_membership()
        {
            _membershipService.Register("login", "pass");
            _membershipFactory.Verify(mf => mf.Create(_userRegistered, "login", "pass", It.IsAny<DateTime>()));

            _membershipService.Register(_user, "login", "pass");
            _membershipFactory.Verify(mf => mf.Create(_user, "login", "pass", It.IsAny<DateTime>()));
        }

    }
}