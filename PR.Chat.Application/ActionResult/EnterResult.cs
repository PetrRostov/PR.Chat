using System;
using System.Runtime.Serialization;
using PR.Chat.Application.DTO;
using PR.Chat.Infrastructure;

namespace PR.Chat.Application
{
    [DataContract]
    [Serializable]
    public class EnterResult
    {
        [DataMember]
        public MembershipDto Membership { get; private set; }

        [DataMember]
        public NickDto Nick { get; private set; }

        [DataMember]
        public bool WithNick { get; private set; }

        public EnterResult(MembershipDto membership, NickDto nick)
        {
            Membership  = membership;
            Nick        = nick;
            WithNick    = nick == null;
        }
    }
}