using System;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    public class Membership : IEntity<Membership, Guid>
    {
        private readonly Guid _id;

        private readonly string _login;

        private readonly DateTime _registeredAt;

        private readonly User _user;

        private string _password;

        private readonly bool _isTemporary;

        public virtual DateTime RegisteredAt { get { return _registeredAt; } }

        public virtual DateTime? LastLogin { get; set; }

        public virtual string Login { get { return _login; } }

        public virtual Guid Id { get { return _id; } }

        public virtual User User { get { return _user; } }

        public virtual bool IsTemporary {get { return _isTemporary; }}

        internal Membership(User user, string login, string password, DateTime registeredAt, bool isTemporary = false)
        {
            Require.NotNull(user, "user");
            Require.NotNullOrEmpty(login, "login");
            Require.NotNullOrEmpty(password, "password");

            _user           = user;
            _login          = login;
            _registeredAt   = registeredAt;
            _isTemporary    = isTemporary;
            _password       = password;
        }

        public virtual bool IsPasswordEqual(string password)
        {
            return _password == password;
        }

        public virtual void SetPassword(string password)
        {
            Require.NotNullOrEmpty(password, "password");
            _password = password;
        }

        protected Membership()
        {
            // for NHibernate
        }

        

        public virtual bool SameIdentityAs(Membership other)
        {
            return other != null && other.Login.Equals(Login, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return _login.ToLowerInvariant().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            return SameIdentityAs((Membership)obj);
        }
    }
}