using System;
using System.Collections.Generic;

namespace PR.Chat.Core.BusinessObjects
{
    public interface IChannel : IEntity
    {
        string Name { get; }

        string Subject { get; }

        INick Owner { get; }

        bool IsHidden { get; }

        bool IsTemporary { get; }

        IEnumerable<INick> Nicks { get; }

        DateTime CreatedAt { get; }
    }

}