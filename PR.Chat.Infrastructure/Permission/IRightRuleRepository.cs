using System;
using System.Collections.Generic;

namespace PR.Chat.Infrastructure
{
    public interface IRightRuleRepository
    {
        IEnumerable<RightRule> GetByOwnerId(Guid ownerId);
    }
}