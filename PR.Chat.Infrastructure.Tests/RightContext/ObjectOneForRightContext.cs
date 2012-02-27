using System;
using PR.Chat.Infrastructure.RightContext;

namespace PR.Chat.Infrastructure.Tests
{
    [RightContextMember]
    public class ObjectOneForRightContext
    {
        [RequiredPermission("Update", "i", "str", SpecialArgument.This)]
        public string Update(string str, int i, DateTime updatedAt)
        {
            return str + i.ToString() + updatedAt.ToString();
        }

        [RequiredPermission("Receive", SpecialArgument.This, "opa")]
        public string Receive(string opa)
        {
            return string.Empty;
        }
    }
}