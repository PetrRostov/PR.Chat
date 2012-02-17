using System;
using PR.Chat.Infrastructure.LinqSpecs;

namespace PR.Chat.Domain
{
    public static class MembershipSpecification
    {
        public static Specification<Membership> LoginMustEquals(string login)
        {
           //var loginLower = login.ToLowerInvariant();
           return new AdHocSpecification<Membership>(
                m => m.Login.ToLowerInvariant().Equals(login.ToLowerInvariant())
           );
        }

        public static Specification<Membership> UserOwnerMustEquals(User user)
        {
            return new AdHocSpecification<Membership>(
               m => m.User.Id == user.Id
            );
        }
    }

}