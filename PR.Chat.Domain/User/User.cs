using System;
using System.Collections.Generic;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    public class User : IEntity<User, Guid>
    {
        private readonly Guid _id;
        private readonly string _name;
        private string _password;
        private readonly bool _isRegistered;
        private readonly ICollection<Nick> _nicks = new HashSet<Nick>();

        public virtual Guid Id { get { return _id; } }

        public virtual string Name { get { return _name; }  }

        public virtual ICollection<Nick> Nicks { get { return _nicks; } }

        public virtual bool IsRegistered  { get { return _isRegistered; } }


        protected User() 
        {
            // For NHibernate
        }

        internal User(
            string name, 
            string password, bool isRegistered)
        {
            Check.NotNullOrEmpty(name, "name");
            Check.NotNullOrEmpty(password, "password");

            _name = name;
            _password = password;
            _isRegistered = isRegistered;
            //_id = Guid.NewGuid();
        }

        public virtual bool IsPasswordEqual(string password)
        {
            return _password == password;
        }

        public virtual bool SameIdentityAs(User other)
        {
            return other != null && other.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase);
        }


        public override int GetHashCode()
        {
            return Name.ToLowerInvariant().GetHashCode();
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

            return new Nick(this, name);
        }

        public virtual void SetPassword(string password)
        {
            Check.NotNullOrEmpty(password, "password");
            _password = password;
        }
    }
}
