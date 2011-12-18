using System;

namespace PR.Chat.Infrastructure.Data
{
    public class EntityNotFoundException<T> : Exception
    {
        public EntityNotFoundException()
        {
            
        }

        public EntityNotFoundException(string message)
            : base(message) 
        {
            
        }
    }
}