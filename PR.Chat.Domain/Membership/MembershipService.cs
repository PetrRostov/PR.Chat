using System;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMembershipFactory _membershipFactory;
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;

        public MembershipService(
            IMembershipRepository membershipRepository, 
            IMembershipFactory membershipFactory, 
            IUserRepository userRepository, 
            IUserFactory userFactory
        )
        {
            Check.NotNull(membershipRepository, "membershipRepository");
            Check.NotNull(userRepository, "userRepository");
            Check.NotNull(userFactory, "userFactory");
            Check.NotNull(membershipFactory, "membershipFactory");

            _membershipRepository = membershipRepository;
            _membershipFactory = membershipFactory;
            _userRepository = userRepository;
            _userFactory = userFactory;
        }

        public Membership Register(User user, string login, string password)
        {
            Check.NotNullOrEmpty(login, "login");
            Check.NotNullOrEmpty(password, "password");


            user.SetAsRegistered();
            
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
    }
}