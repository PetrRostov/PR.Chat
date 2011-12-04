using System;

namespace PR.Chat.Domain
{
    public class MembershipFactory : IMembershipFactory
    {
        public Membership Create(User user, string login, string password, DateTime registeredAt)
        {
            return new Membership(
                user,
                login,
                password,
                registeredAt
                );
        }
    }
}