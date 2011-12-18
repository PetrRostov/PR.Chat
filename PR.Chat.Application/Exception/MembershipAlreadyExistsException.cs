using System;

namespace PR.Chat.Application
{
    public class MembershipAlreadyExistsException : Exception
    {
        public MembershipAlreadyExistsException(string email)
            : base(string.Format("Membership with login={0} already exists", email))
        {
            
        }
    }
}