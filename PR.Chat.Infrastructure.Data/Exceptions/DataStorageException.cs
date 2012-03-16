using System;

namespace PR.Chat.Infrastructure.Data
{
    public class DataStorageException : Exception
    {
        public DataStorageException()
        {
            
        }

        public DataStorageException(string message)
            :base(message)
        {
            
        }

        public DataStorageException(string message, Exception innerException)
            :base(message, innerException)
        {
            
        }
    }
}