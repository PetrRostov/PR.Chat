using PR.Chat.Core.BusinessObjects;

namespace PR.Chat.Core
{
    public class NickCreateResult : ActionResult
    {
        INick Nick { get; set; }
    }
}