using System;
using PR.Chat.Application.DTO;
using PR.Chat.Domain;
using PR.Chat.Infrastructure;
using PR.Chat.Infrastructure.Data;
using PR.Chat.Infrastructure.UnitOfWork;


namespace PR.Chat.Application
{
    public class MembershipService : IMembershipService
    {
        private readonly Domain.IMembershipService _membershipService;
        private readonly IUserRepository _userRepository;
        private readonly INickRepository _nickRepository;
        private readonly IUserFactory _userFactory;
        private readonly IMappingProvider _mappingProvider;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMembershipFactory _membershipFactory;

        public MembershipService(
            Domain.IMembershipService membershipService,
            IUserRepository userRepository,
            INickRepository nickRepository,
            IUserFactory userFactory,
            IMappingProvider mappingProvider,
            IMembershipRepository membershipRepository,
            IMembershipFactory membershipFactory
        )
        {
            Check.NotNull(membershipService, "membershipService");
            Check.NotNull(userRepository, "userRepository");
            Check.NotNull(nickRepository, "nickRepository");
            Check.NotNull(userFactory, "userFactory");
            Check.NotNull(mappingProvider, "mappingProvider");
            Check.NotNull(membershipRepository, "membershipRepository");
            Check.NotNull(membershipFactory, "membershipFactory");

            _membershipService = membershipService;
            _userRepository = userRepository;
            _nickRepository = nickRepository;
            _userFactory = userFactory;
            _mappingProvider = mappingProvider;
            _membershipRepository = membershipRepository;
            _membershipFactory = membershipFactory;
        }

        public EnterResult EnterAsUnregistered(string nickName)
        {
            if (_nickRepository.ExistsWithName(nickName))
                throw new NickAlreadyExistsException();

            using (var unitOfWork = UnitOfWork.Start())
            {
                var user = _userFactory.CreateUnregistered();
                _userRepository.Add(user);

                var nick = user.CreateNick(nickName);
                _nickRepository.Add(nick);

                user.Nicks.Add(nick);

                var membership = _membershipFactory.CreateTemporary(user);

                unitOfWork.Commit();

                return new EnterResult(
                    _mappingProvider.GetMapping<Membership, MembershipDto>().Convert(membership),
                    _mappingProvider.GetMapping<Nick, NickDto>().Convert(nick)
                );
            }
        }

        public EnterResult EnterAsRegisteredByNick(string nickName, string password)
        {
            using (var unitOfWork = UnitOfWork.Start())
            {
                var nick = _nickRepository.GetByName(nickName);
                var memberships = _membershipRepository.GetByUser(nick.User);

                var isPasswordCorrect = false;
                Membership membership = null;

                foreach (var m in memberships)
                    if (m.IsPasswordEqual(password))
                    {
                        isPasswordCorrect = true;
                        membership = m;
                        break; ;
                    }

                if (!isPasswordCorrect)
                    throw new PasswordIncorrectException();

                membership.LastLogin = DateTime.UtcNow;

                unitOfWork.Commit();

                return new EnterResult(
                    _mappingProvider.GetMapping<Membership, MembershipDto>().Convert(membership),
                    _mappingProvider.GetMapping<Nick, NickDto>().Convert(nick)
                );
    
            }
        }

        public EnterResult EnterAsRegistered(string membershipLogin, string password)
        {
            using (var unitOfWork = UnitOfWork.Start())
            {
                Membership membership = _membershipRepository.GetByLogin(membershipLogin);

                if (!membership.IsPasswordEqual(password))
                    throw new PasswordIncorrectException();

                membership.LastLogin = DateTime.UtcNow;

                unitOfWork.Commit();

                return new EnterResult(
                    _mappingProvider.GetMapping<Membership, MembershipDto>().Convert(membership),
                    null
                );
            }
        }

        public RegisterResult Register(string email, string password, string repeatPassword)
        {
            if (!password.Equals(repeatPassword))
                throw new PasswordsNotEqualsException();

            if (_membershipRepository.ExistsWithLogin(email))
                throw new MembershipAlreadyExistsException(email);


            Membership membership;

            using (var unitOfWork = UnitOfWork.Start())
            {
                membership = _membershipService.Register(email, password);

                unitOfWork.Commit();
            }

            return new RegisterResult {
                Membership = _mappingProvider.GetMapping<Membership, MembershipDto>().Convert(membership)
            };
        }

        public RegisterResult Register(string email, string password, string repeatPassword, string nickName)
        {
            if (!password.Equals(repeatPassword))
                throw new PasswordsNotEqualsException();

            if (_membershipRepository.ExistsWithLogin(email))
                throw new MembershipAlreadyExistsException(email);

            Membership membership;

            using (var unitOfWork = UnitOfWork.Start())
            {
                var user = _userFactory.CreateUnregistered();
                _userRepository.Add(user);

                var nick = user.CreateNick(nickName);
                _nickRepository.Add(nick);

                membership = _membershipService.Register(user, email, password);

                unitOfWork.Commit();
            }

            return new RegisterResult
            {
                Membership = _mappingProvider.GetMapping<Membership, MembershipDto>().Convert(membership)
            };
        }
            
    }
}