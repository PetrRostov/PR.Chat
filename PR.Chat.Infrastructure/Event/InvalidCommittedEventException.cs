// ------------------------------------------------
// This code was taken from «Ncqrs Framework» project at
// http://ncqrs.org/
// ------------------------------------------------
using System;
using System.Runtime.Serialization;

namespace PR.Chat.Infrastructure
{
    [Serializable]
    public class InvalidCommittedEventException : Exception
    {
        public InvalidCommittedEventException()
        {
        }

        public InvalidCommittedEventException(string message)
            : base(message)
        {
        }

        public InvalidCommittedEventException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InvalidCommittedEventException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {

        }
    }
}