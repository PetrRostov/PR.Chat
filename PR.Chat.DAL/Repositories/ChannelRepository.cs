using System;
using System.Linq;
using System.Collections.Generic;
using PR.Chat.Core.BusinessObjects;
using PR.Chat.Core.Repositories;
using PR.Chat.DAL.EF;

namespace PR.Chat.DAL.Repositories
{
    public class ChannelRepository : BaseRepository<IChannel, Channel>, IChannelRepository
    {
        public ChannelRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public ChannelRepository(IDatabase database) : base(database)
        {
        }

        public IEnumerable<IChannel> GetAll()
        {
            return GetSource().AsEnumerable();
        }
    }
}