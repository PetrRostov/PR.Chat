using System;
using PR.Chat.Infrastructure.LinqSpecs;

namespace PR.Chat.Domain
{
    public static class MembershipSpecification
    {
         public static Specification<Membership> LoginEquals(string login)
         {
             return new AdHocSpecification<Membership>(
                m =>  login.Equals(m.Login, StringComparison.InvariantCultureIgnoreCase)
             );
         }
    }

}