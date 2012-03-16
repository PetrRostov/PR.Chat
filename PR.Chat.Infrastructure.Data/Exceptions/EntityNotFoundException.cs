using System;

namespace PR.Chat.Infrastructure.Data
{
    public class EntityNotFoundException : DataStorageException
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