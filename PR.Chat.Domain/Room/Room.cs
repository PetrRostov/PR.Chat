using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PR.Chat.Domain;
using PR.Chat.Infrastructure;


// Rule
//Rule<ReceivePermission>

//ExpiredAt


//bool Check(Nick )
//{
    
//}

// Message m => m.From.Equals()
// AddRule, RemoveRule, ClearAllRules, 
// Dictionary<IPermission,, IRule>
// Правило: такому-то пользователю нельзя разговаривать 15 секунд
// [RequiredPermission(ReceivePermission)]
// ReceiveMessage(Message message)
// субъект — всегда пользователь
// объект  — право разговора
// это правило действует до 2012-01-01

// Правило: пользователю Опа добавил в игнор пользователя Хуепа
// субъект — Опа
// объект — право получать сообщение от Хуепа
// никогда не истекает

// guest => true

// общие правила канала
// правила канала для пользователя



namespace PR.Chat.Domain
{
    public class Room : IEntity<Room, Guid>
    {
        private Guid _id;
        private readonly string _name;
        private string _lowerName;
        private string _description;
        private bool _isTemporary;
        private readonly DateTime _createdAt;
        private readonly ICollection<Nick> _nicks;

        public virtual Guid Id {get { return _id; }}

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

        public bool SameIdentityAs(Room other)
        {
            return other != null && Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase);
        }

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

        
    }
}