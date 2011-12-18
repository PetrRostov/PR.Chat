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
            var membership = GetSource()
                .Where(MembershipSpecification.LoginEquals(login).IsSatisfiedBy())
                .FirstOrDefault();

            if (membership == null)
                ExceptionHelper.EntityNotFound<Membership>("login", login);

            return membership;
        }

        public IEnumerable<Membership> GetByUser(User user)
        {
            return GetSource()
                .Where(MembershipSpecification.UserOwnerEquals(user).IsSatisfiedBy())
                .ToList()
                .AsReadOnly();
        }

        public bool ExistsWithLogin(string login)
        {
            var membership = GetSource()
                .Where(MembershipSpecification.LoginEquals(login).IsSatisfiedBy())
                .FirstOrDefault();

            return membership != null;
        }
    }
}