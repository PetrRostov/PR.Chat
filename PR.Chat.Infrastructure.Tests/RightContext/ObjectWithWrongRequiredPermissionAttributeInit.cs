using System;
using PR.Chat.Infrastructure.RightContext;

namespace PR.Chat.Infrastructure.Tests
{
    [RightContextMember]
    public class ObjectWithWrongRequiredPermissionAttributeInit
    {
        [RequiredPermission("Change", "opa")]
        public string ChangeOpa(string str, int i, Guid uin)
        {
            return string.Empty;
        }
    }
}