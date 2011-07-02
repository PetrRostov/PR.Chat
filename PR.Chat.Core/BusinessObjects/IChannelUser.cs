using System.Collections.Generic;

namespace PR.Chat.Core.BusinessObjects
{
    public interface IChannelUser
    {
        INick Nick { get; }

        IDictionary<string, object> Properties { get; }
    }
}