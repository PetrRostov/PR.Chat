using System;

namespace PR.Chat.Infrastructure
{
    public interface IEvent
    {
        Guid Id { get; }

        Guid SourceId { get; }

        Guid CommitId { get; }

        int Version { get; }

        DateTime OccuredAt { get; }

        long SequenceNumber { get; }

        object Payload { get; }
    }
}