using System;
using System.Collections.Generic;

namespace PR.Chat.Infrastructure.RightContext
{
    public interface IRightRuleRepository
    {
        IEnumerable<RightRule> Get(Guid ownerId);

        IEnumerable<RightRule> Get(Guid ownerId, string permission);
    }
}