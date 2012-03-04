using System;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipFactory _membershipFactory;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IUserFactory _userFactory;
        private readonly IUserRepository _userRepository;

        public MembershipService(
            IMembershipRepository membershipRepository, 
            IMembershipFactory membershipFactory, 
            IUserRepository userRepository, 
            IUserFactory userFactory
        )
        {
            Require.NotNull(membershipRepository, "membershipRepository");
            Require.NotNull(userRepository, "userRepository");
            Require.NotNull(userFactory, "userFactory");
            Require.NotNull(membershipFactory, "membershipFactory");

            _membershipRepository = membershipRepository;
            _membershipFactory = membershipFactory;
            _userRepository = userRepository;
            _userFactory = userFactory;
        }

        #region IMembershipService Members

        public Membership Register(User user, string login, string password)
        {
            Require.NotNullOrEmpty(login, "login");
            Require.NotNullOrEmpty(password, "password");


            user.SetIsRegistered();
            
            var membership = _membershipFactory.Create(user, login, password, DateTime.UtcNow);
            _membershipRepository.Add(membership);

            return membership;
        }

        public Membership Register(string login, string password)
        {
            var user = _userFactory.CreateRegistered();
            _userRepository.Add(user);

            return Register(user, login, password);
        }

        #endregion
    }
}