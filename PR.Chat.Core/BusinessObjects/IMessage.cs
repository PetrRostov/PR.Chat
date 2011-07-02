using System;

namespace PR.Chat.Core.BusinessObjects
{
    public interface IMessage : IEntity
    {
        INick From { get; }

        DateTime CreatedAt { get; }

        string Text { get; }
    }
}