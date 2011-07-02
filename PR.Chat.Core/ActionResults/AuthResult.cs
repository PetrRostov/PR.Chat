using PR.Chat.Core.BusinessObjects;

namespace PR.Chat.Core
{
    public class AuthResult : ActionResult
    {
        public IAccount Account { get; set; }
    }
}