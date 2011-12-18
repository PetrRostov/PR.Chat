using System;

namespace PR.Chat.Domain
{
    public class MembershipAlreadyExistsException : Exception
    {
        public MembershipAlreadyExistsException()
        {
            
        }

        public MembershipAlreadyExistsException(string message)
            : base(message)
        {
            
        }
    }
}