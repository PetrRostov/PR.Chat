using System;
using System.Collections.Generic;

namespace PR.Chat.Core.BusinessObjects
{
    public interface IAccount : IEntity
    {
        string Login { get; }

        IEnumerable<INick> Nicks { get; }

        DateTime CreatedAt { get; }
    }
}