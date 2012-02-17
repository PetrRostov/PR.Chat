using System;

namespace PR.Chat.Domain
{
    public class PermissionNotFoundException : Exception
    {
        public PermissionNotFoundException()
        {
            
        }

        public PermissionNotFoundException(string message)
            : base(message)
        {
            
        }
    }
}