using System;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    public class Message: IEntity<Message, Guid>
    {
        private Guid _id;

        private DateTime _sentAt;
        public virtual DateTime SentAt
        {
            get { return _sentAt; }
        }

        private FormattedText _formattedText;
        public virtual FormattedText FormattedText
        {
            get { return _formattedText; }
            set { _formattedText = value; }
        }

        public Guid Id
        {
            get { return _id; }
        }

        public bool SameIdentityAs(Message other)
        {
            if (other == null)
                return false;

            return other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            return SameIdentityAs((Message) obj);
        }
    }
}