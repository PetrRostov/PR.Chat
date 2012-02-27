using System;

namespace PR.Chat.Infrastructure.RightContext
{
    public class RightException : Exception
    {
        public RightException(string message)
            : base(message)
        {
            
        }

        public RightException()
        {
            
        }
    }
}