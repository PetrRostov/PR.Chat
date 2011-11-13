using System;

namespace PR.Chat.Domain
{
    public class UserFactory : IUserFactory
    {
        public User CreateUnregistered()
        {
            return new User(
                false
            );
        }

        public User CreateRegistered()
        {
            return new User(true);
        }
    }
}