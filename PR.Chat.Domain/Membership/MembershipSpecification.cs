using System;
using PR.Chat.Infrastructure.LinqSpecs;

namespace PR.Chat.Domain
{
    public static class MembershipSpecification
    {
         public static Specification<Membership> LoginEquals(string login)
         {
             var loginLower = login.ToLowerInvariant();
             return new AdHocSpecification<Membership>(
                m =>  m.Login.ToLowerInvariant().Equals(loginLower)
             );
         }

         public static Specification<Membership> UserOwnerEquals(User user)
         {
             return new AdHocSpecification<Membership>(
                m => m.User.Id == user.Id
             );
         }
    }

}