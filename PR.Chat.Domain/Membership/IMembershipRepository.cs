using System;
using System.Collections.Generic;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    public interface IMembershipRepository : IRepository<Membership, Guid>
    {
        Membership GetByLogin(string login);

        IEnumerable<Membership> GetByUser(User user);

        bool ExistsWithLogin(string login);
    }
}