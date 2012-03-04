using System;
using System.Collections.Generic;
using PR.Chat.Infrastructure;
using PR.Chat.Infrastructure.RightContext;

namespace PR.Chat.Domain
{
    [RightContextMember]
    public class User : IEntity<User, Guid>
    {
        private readonly Guid _id;
        private readonly ICollection<Nick> _nicks = new HashSet<Nick>();
        private bool _isRegistered;


        protected User()
        {
            // For NHibernate
        }

        internal User(bool isRegistered)
        {
            _isRegistered = isRegistered;
            //_id = Guid.NewGuid();
        }

        public virtual ICollection<Nick> Nicks
        {
            get { return _nicks; }
        }

        public virtual bool IsRegistered { get { return _isRegistered; } }

        #region IEntity<User,Guid> Members

        public virtual Guid Id { get { return _id; } }


        public virtual bool SameIdentityAs(User other)
        {
            return other != null && other.Id == Id;
        }

        #endregion

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            return SameIdentityAs((User)obj);
        }

        public virtual Nick CreateNick(string name)
        {
            Require.NotNullOrEmpty(name, "name");

            var nick = new Nick(this, name);

            _nicks.Add(nick);

            return nick;
        }

        public virtual void SetIsRegistered()
        {
            
            _isRegistered = true;
        }
    }
}
