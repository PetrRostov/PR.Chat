using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PR.Chat.Application.DTO
{
    public class UserDto
    {
        public string Id { get; set; }

        public ReadOnlyCollection<NickDto> Nicks { get; set; }
    }
}