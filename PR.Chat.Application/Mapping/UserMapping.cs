using System.Linq;
using PR.Chat.Application.DTO;
using PR.Chat.Domain;

namespace PR.Chat.Application
{
    public class UserMapping : IMapping<User, UserDto>
    {
        private readonly IMappingProvider _mappingProvider;

        public UserMapping(IMappingProvider mappingProvider)
        {
            _mappingProvider = mappingProvider;
        }

        public UserDto Convert(User from)
        {
            var nickMapper = _mappingProvider.GetMapping<Nick, NickDto>();

            return new UserDto {
                Id      = from.Id.ToString(),
                Nicks   = from.Nicks.Select(nickMapper.Convert)
            };
        }
    }
}