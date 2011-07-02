using System.Collections.Generic;
using PR.Chat.Core.BusinessObjects;

namespace PR.Chat.Core.Repositories
{
    public interface IMessageRepository : IRepository<IMessage>
    {
        IEnumerable<IChannelMessage> GetLastByChannel(IChannel channel, int last);

        IEnumerable<IPrivateMessage> GetLastPrivate(INick toNick, int last);
    }
}