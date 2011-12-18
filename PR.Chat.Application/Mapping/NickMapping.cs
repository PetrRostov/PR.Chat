using PR.Chat.Application.DTO;
using PR.Chat.Domain;

namespace PR.Chat.Application
{
    public class NickMapping : IMapping<Nick, NickDto>
    {
        public NickDto Convert(Nick from)
        {
            return new NickDto {
                Id      = from.Id.ToString(),
                Name    = from.Name
            };
        }
    }
}