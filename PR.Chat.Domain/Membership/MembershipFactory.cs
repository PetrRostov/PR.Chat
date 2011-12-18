using System;

namespace PR.Chat.Domain
{
    public class MembershipFactory : IMembershipFactory
    {
        private const string AnonymousLogin         = "Unregistered";
        private const string PasswordForTemporary   = "6C54B89E-921E-4F89-AFCF-76B6435CEA0D";
        
        public Membership Create(User user, string login, string password, DateTime registeredAt)
        {
            return new Membership(
                user,
                login,
                password,
                registeredAt,
                false
            );
        }

        public Membership CreateTemporary(User user)
        {
            return new Membership(
                user,
                AnonymousLogin,
                PasswordForTemporary,
                DateTime.UtcNow,
                true
            );
        }
    }
}