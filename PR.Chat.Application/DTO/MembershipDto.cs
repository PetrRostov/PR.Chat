using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PR.Chat.Application.DTO
{
    [DataContract]
    public class MembershipDto
    {
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public UserDto User { get; set; }
    }
}