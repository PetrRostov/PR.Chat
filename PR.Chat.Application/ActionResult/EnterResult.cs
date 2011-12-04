using PR.Chat.Application.DTO;
using PR.Chat.Infrastructure;

namespace PR.Chat.Application
{
    public class EnterResult
    {
        public MembershipDto Membership { get; private set; }

        public NickDto Nick { get; private set; }

        public bool WithNick { get; private set; }

        public EnterResult(MembershipDto membership, NickDto nick)
        {
            Membership  = membership;
            Nick        = nick;
            WithNick    = nick == null;
        }
    }
}