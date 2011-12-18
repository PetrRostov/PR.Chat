using System;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    public interface INickRepository : IRepository<Nick, Guid>
    {
        Nick GetByName(string nickName);

        bool ExistsWithName(string nickName);
    }
}