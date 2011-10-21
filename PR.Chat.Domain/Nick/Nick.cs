using System;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    public class Nick : IEntity<Nick, Guid>
    {
        private readonly User _user;
        private readonly string _name;
        private readonly Guid _id;

        protected Nick()
        {
            // For NHibernate
        }

        internal Nick(User user, string name)
        {
            _user = user;
            _name = name;
            _id = Guid.NewGuid();
        }

        public virtual string Name      
        {
            get { return _name; }
        }

        public virtual Guid Id
        {
            get {
                return _id;
            }
        }

        public bool SameIdentityAs(Nick other)
        {
            return other != null && Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Name.ToLowerInvariant().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            return SameIdentityAs((Nick) obj);
        }
    }
}