using System;

namespace PR.Chat.Domain
{
    public interface IMembershipFactory
    {
        Membership Create(
            User user, 
            string login, 
            string password, 
            DateTime registeredAt
        );
    }
}