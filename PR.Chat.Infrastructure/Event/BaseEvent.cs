using System;

namespace PR.Chat.Infrastructure
{
    public abstract class BaseEvent : IEvent
    {
        #region Implementation of IEvent

        private readonly Guid _id;
        private readonly Guid _sourceId;
        private readonly Guid _commitId;
        private readonly int _version;
        private readonly DateTime _occuredAt;
        private readonly long _sequenceNumber;
        private readonly object _payload;

        public BaseEvent(Guid id, Guid sourceId, Guid commitId, int version, DateTime occuredAt, long sequenceNumber, object payload)
        {
            _id = id;
            _sourceId = sourceId;
            _commitId = commitId;
            _version = version;
            _occuredAt = occuredAt;
            _sequenceNumber = sequenceNumber;
            _payload = payload;
        }

        public Guid Id
        {
            get { return _id; }
        }

        public Guid SourceId
        {
            get { return _sourceId; }
        }

        public Guid CommitId
        {
            get { return _commitId; }
        }

        public int Version
        {
            get { return _version; }
        }

        public DateTime OccuredAt
        {
            get { return _occuredAt; }
        }

        public long SequenceNumber
        {
            get { return _sequenceNumber; }
        }

        public object Payload
        {
            get { return _payload; }
        }

        #endregion
    }
}