using PR.Chat.Application.DTO;
using PR.Chat.Domain;

namespace PR.Chat.Application
{
    public class MembershipMapping : IMapping<Membership, MembershipDto>
    {
        private readonly IMappingProvider _mappingProvider;

        public MembershipMapping(IMappingProvider mappingProvider)
        {
            _mappingProvider = mappingProvider;
        }

        public MembershipDto Convert(Membership from)
        {
            return new MembershipDto {
                Login   = from.Login,
                User    = _mappingProvider.GetMapping<User, UserDto>().Convert(from.User)
            };
        }
    }
}