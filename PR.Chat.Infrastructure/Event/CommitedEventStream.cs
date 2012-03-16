// ------------------------------------------------
// This code was taken from «Ncqrs Framework» project at
// http://ncqrs.org/
// ------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PR.Chat.Infrastructure
{
    public class CommittedEventStream : IEnumerable<CommittedEvent>
    {
        private readonly long _fromVersion;
        private readonly long _toVersion;
        private readonly Guid _sourceId;
        private readonly List<CommittedEvent> _events = new List<CommittedEvent>();

        public long FromVersion
        {
            get { return _fromVersion; }
        }

        public long ToVersion
        {
            get { return _toVersion; }
        }

        public bool IsEmpty
        {
            get { return _events.Count == 0; }
        }

        public Guid SourceId
        {
            get { return _sourceId; }
        }

        public long CurrentSourceVersion
        {
            get { return _toVersion; }
        }

        public IEnumerator<CommittedEvent> GetEnumerator()
        {
            return _events.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public CommittedEventStream(Guid sourceId)
        {
            _sourceId = sourceId;
        }

        public CommittedEventStream(Guid sourceId, params CommittedEvent[] events)
            : this(sourceId, (IEnumerable<CommittedEvent>)events)
        {
        }

        public CommittedEventStream(Guid sourceId, IEnumerable<CommittedEvent> events)
        {
            _sourceId = sourceId;

            if (events != null) _events = new List<CommittedEvent>(events);

            ValidateEventInformation(_events);

            if (_events.Count > 0)
            {
                var first = _events.First();
                _fromVersion = first.SequenceNumber;

                var last = _events.Last();
                _toVersion = last.SequenceNumber;

                _toVersion = _events.OrderByDescending(evnt => evnt.SequenceNumber).First().SequenceNumber;
            }
        }

        private void ValidateEventInformation(IEnumerable<CommittedEvent> events)
        {
            // An empty event stream is allowed.
            if (events == null || events.Count() == 0) return;

            var firstEvent = events.First();
            var startSequence = firstEvent.SequenceNumber;

            var expectedSourceId = _sourceId;
            var expectedSequence = startSequence;

            ValidateSingleEvent(firstEvent, 0, expectedSourceId, expectedSequence);

            for (int position = 1; position < _events.Count; position++)
            {
                var evnt = _events[position];
                expectedSourceId = _sourceId;
                expectedSequence = startSequence + position;

                ValidateSingleEvent(evnt, position, expectedSourceId, expectedSequence);
            }
        }

        private void ValidateSingleEvent(CommittedEvent evnt, long position, Guid expectedSourceId, long expectedSequence)
        {
            if (evnt == null)
                throw new ArgumentNullException("events", "The events stream contains a null reference at position " + position + ".");

            if (evnt.SourceId != expectedSourceId)
            {
                var msg = string.Format("The events stream contains an event that is related to another event " +
                                        "source at position {0}. Expected event source id {1}, but actual was {2}",
                                        position, _sourceId, evnt.SourceId);
                throw new ArgumentException("events", msg);
            }

            if (evnt.SequenceNumber != expectedSequence)
            {
                var msg =
                    string.Format("The events stream contains an committed event with an illigal sequence at " +
                                  "position {0}. The expected sequence is {1}, but actual was {2}.",
                                  position, expectedSequence, evnt.SequenceNumber);

                throw new ArgumentException("events", msg);
            }
        }
    }
}