using System;
using System.Collections.Generic;
using System.Linq;
using PR.Chat.Domain;

namespace PR.Chat.Infrastructure.Data
{
    public class MembershipRepository : BaseRepository<Membership, Guid>, IMembershipRepository
    {
        public MembershipRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public MembershipRepository(IDatabase database) : base(database)
        {
        }

        public Membership GetByLogin(string login)
        {
            return GetSource()
                .Where(MembershipSpecification.LoginEquals(login).IsSatisfiedBy())
                .First();
        }
    }
}