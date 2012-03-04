using System;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    public class Session : IEntity<Session, Guid>
    {
        private readonly Guid _id;
        private readonly Guid _ownerId;

        internal Session(Guid id, Guid ownerId)
        {
            _ownerId = ownerId;
            _id = id;
        }

        public Guid OwnerId
        {
            get { return _ownerId; }
        }

        #region IEntity<Session,Guid> Members

        public virtual Guid Id
        {
            get { return _id; }
        }

        #endregion

        #region Implementation of IEntity<in Session,out Guid>

        public bool SameIdentityAs(Session other)
        {
            if (other == null)
                return false;

            return OwnerId == other.OwnerId;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            return SameIdentityAs((Session)obj);
        }

        public override int GetHashCode()
        {
            return OwnerId.GetHashCode();
        }
    }
}