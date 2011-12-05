using System;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    public interface IMembershipRepository : IRepository<Membership, Guid>
    {
        Membership GetByLogin(string login);


    }
}