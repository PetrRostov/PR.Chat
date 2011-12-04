using System.Collections.Generic;

namespace PR.Chat.Application.DTO
{
    public class MembershipDto
    {
        public string Login { get; set; }
        public UserDto User { get; set; }
    }
}