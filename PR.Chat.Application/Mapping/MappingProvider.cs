using System.Collections.Generic;
using PR.Chat.Application.DTO;
using PR.Chat.Domain;

namespace PR.Chat.Application
{
    public class MappingProvider : IMappingProvider
    {

        private readonly IDictionary<MappingKey, object> _mappings;

        public MappingProvider()
        {
            _mappings = new Dictionary<MappingKey, object> {
                {new MappingKey(typeof(User), typeof(UserDto)), new UserMapping(this)},
                {new MappingKey(typeof(Nick), typeof(NickDto)), new NickMapping()},
                {new MappingKey(typeof(Membership), typeof(MembershipDto)), new MembershipMapping(this)}
            };
        }

        public IMapping<TFrom, TTo> GetMapping<TFrom, TTo>()
        {
            var mappingKey = new MappingKey(typeof (TFrom), typeof (TTo));
            if (!_mappings.ContainsKey(mappingKey))
                throw new MappingNotExistsException(typeof(TFrom), typeof(TTo));

            return (IMapping<TFrom, TTo>)_mappings[mappingKey];
        }
    }
}