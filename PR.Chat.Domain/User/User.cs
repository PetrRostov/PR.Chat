using System;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    public class User : IEntity<User>
    {
        private readonly Guid _id;
        private readonly string _name;
        private readonly string _password;

        protected User() {}

        public virtual Guid Id { get { return _id; } }

        public virtual string Name { get { return _name; }  }

        
        internal User(
            string name, 
            string password
        )
        {
            Check.NotNullOrEmpty(name, "name");
            Check.NotNullOrEmpty(password, "password");

            _name = name;
            _password = password;
            _id = Guid.NewGuid();
        }

        public bool IsPasswordEqual(string password)
        {
            return _password == password;
        }

        public bool SameIdentityAs(User other)
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

        public Nick CreateNick(string name)
        {
            return new Nick(this, name);
        }
    }
}
