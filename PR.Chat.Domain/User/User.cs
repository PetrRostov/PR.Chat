using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    public class User : IEntity<User, Guid>
    {
        private readonly Guid _id;
        private bool _isRegistered;
        private readonly ICollection<Nick> _nicks = new HashSet<Nick>();

        public virtual Guid Id { get { return _id; } }

        public virtual ICollection<Nick> Nicks
        {
            get { return _nicks; }
        }

        public virtual bool IsRegistered  { get { return _isRegistered; } }


        protected User() 
        {
            // For NHibernate
        }

        internal User(bool isRegistered)
        {
            _isRegistered = isRegistered;
            //_id = Guid.NewGuid();
        }


        public virtual bool SameIdentityAs(User other)
        {
            return other != null && other.Id == this.Id;
        }


        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            return SameIdentityAs((User) obj);
        }

        public virtual Nick CreateNick(string name)
        {
            Check.NotNullOrEmpty(name, "name");

            var nick = new Nick(this, name);

            _nicks.Add(nick);

            return nick;
        }

        public virtual void SetAsRegistered()
        {
            _isRegistered = true;
        }
    }
}
