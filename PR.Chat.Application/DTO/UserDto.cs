using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace PR.Chat.Application.DTO
{
    [DataContract]
    public class UserDto
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public IEnumerable<NickDto> Nicks { get; set; }
    }
}