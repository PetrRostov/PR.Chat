using System;
using Moq;
using NUnit.Framework;
using PR.Chat.Domain;
using PR.Chat.Test.Common;

namespace PR.Chat.Application.Tests
{
    [Ignore, TestFixture, Category(TestCategory.Application)]
    public class MembershipServiceFixtures
    {
        private Mock<Domain.IMembershipService> _domainMembershipService;
        private Mock<IUserRepository> _userRepostory;
        private Mock<INickRepository> _nickRepository;
        private Mock<IUserFactory> _userFactory;
        private Mock<User> _unregisteredUser;
        private Mock<Nick> _nick;
        private Mock<IMappingProvider> _mappingProvider;
        private Mock<IMembershipRepository> _membershipRepository;
        private Mock<IMembershipFactory> _membershipFactory;

        private IMembershipService _membershipService;

        [SetUp]
        public void Init()
        {
            _unregisteredUser           = new Mock<User>();
            //_unregisteredUser.Setup(u => u.CreateNick("")).Callback<string>()
            _nick                       = new Mock<Nick>(_unregisteredUser.Object, "newNick");

            _unregisteredUser.Setup(u => u.CreateNick(It.IsAny<string>())).Returns(_nick.Object);


            _userRepostory              = new Mock<IUserRepository>();
            _nickRepository             = new Mock<INickRepository>();
            _domainMembershipService    = new Mock<Domain.IMembershipService>();

            _membershipFactory          = new Mock<IMembershipFactory>();

            _userFactory                = new Mock<IUserFactory>();
            _userFactory
                .Setup(u => u.CreateUnregistered())
                .Returns(_unregisteredUser.Object)
                .Verifiable();

            _mappingProvider = new Mock<IMappingProvider>();

            _membershipRepository = new Mock<IMembershipRepository>();

            _membershipService = new MembershipService(
                _domainMembershipService.Object,
                _userRepostory.Object,
                _nickRepository.Object,
                _userFactory.Object,
                _mappingProvider.Object,
                _membershipRepository.Object,
                _membershipFactory.Object
            );
        }

        [Test]
        public void EnterAsUnregistered_should_add_new_nick_and_user_in_repository()
        {
            _userRepostory.Setup(ur => ur.Add(_unregisteredUser.Object)).Verifiable();
            _nickRepository.Setup(nr => nr.Add(_nick.Object)).Verifiable();

            _membershipService.EnterAsUnregistered("nickName");

            _userRepostory.Verify(ur => ur.Add(_unregisteredUser.Object));
            _nickRepository.Verify(nr => nr.Add(_nick.Object));
        }

        [Test]
        public void EnterAsUnregistered_should_throw_exception_if_nick_already_exists()
        {
            
        }

        [Test]
        public void EnterAsUnregistered_should_throw_exception_if_nickName_is_null_or_empty()
        {
            Assert.Throws<ArgumentNullException>(() => _membershipService.EnterAsUnregistered(null));
            Assert.Throws<ArgumentException>(() => _membershipService.EnterAsUnregistered(string.Empty));
        }
    }
}