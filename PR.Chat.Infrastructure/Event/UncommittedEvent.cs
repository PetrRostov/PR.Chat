﻿// ------------------------------------------------
// An idea of this class was taken from «Ncqrs Framework» project at
// http://ncqrs.org/
// ------------------------------------------------
using System;

namespace PR.Chat.Infrastructure
{
    public class UncommittedEvent : BaseEvent
    {
        public UncommittedEvent(Guid id, Guid sourceId, Guid commitId, int version, DateTime occuredAt, long sequenceNumber, object payload) : base(id, sourceId, commitId, version, occuredAt, sequenceNumber, payload)
        {
        }
    }
}