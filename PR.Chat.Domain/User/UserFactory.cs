using System;

namespace PR.Chat.Domain
{
    public class UserFactory : IUserFactory
    {
        public User CreateUnregistered()
        {
            return new User(
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString(), 
                false
            );
        }

        public User CreateRegistered(string name, string password)
        {
            return new User(name, password, true);
        }
    }
}