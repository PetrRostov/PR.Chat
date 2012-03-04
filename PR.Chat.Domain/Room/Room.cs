using System;
using System.Collections.Generic;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    public class Room : IEntity<Room, Guid>
    {
        private readonly DateTime _createdAt;
        private readonly Guid _id;
        private readonly string _name;
        private readonly ICollection<Nick> _nicks;
        private string _description;
        private bool _isTemporary;
        private string _lowerName;

        internal Room(Guid id, string name, string description, bool isTemporary = true)
        {
            Require.NotNullOrEmpty(name, "name");
            _name           = name;
            _isTemporary    = isTemporary;
            _id             = id;
            _createdAt      = DateTime.UtcNow;
            _description    = description ?? string.Empty;
            _nicks          = new HashSet<Nick>();
        }

        protected Room() 
        {
            // For NHibernate    
        }

        public virtual ICollection<Nick> Nicks { get { return _nicks; }}

        public virtual string Name { get { return _name; } }

        public virtual DateTime CreatedAt { get { return _createdAt; } }

        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public virtual bool IsTemporary
        {
            get { return _isTemporary; }
            set { _isTemporary = value; }
        }

        #region IEntity<Room,Guid> Members

        public virtual Guid Id {get { return _id; }}

        public bool SameIdentityAs(Room other)
        {
            return other != null && Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion

        public override int GetHashCode()
        {
            return (_lowerName ?? (_lowerName = Name.ToLowerInvariant()))
                .GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            return SameIdentityAs((Room) obj);
        }

        public override string ToString()
        {
            return this.ToUserString();
        }
    }
}