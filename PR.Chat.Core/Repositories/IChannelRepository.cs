using System.Collections.Generic;
using PR.Chat.Core.BusinessObjects;

namespace PR.Chat.Core.Repositories
{
    public interface IChannelRepository : IRepository<IChannel>
    {
        IEnumerable<IChannel> GetAll();
    }
}