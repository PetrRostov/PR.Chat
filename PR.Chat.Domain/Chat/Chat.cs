using System;
using System.Collections.Generic;
using System.Threading;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    public class Chat : IEntity<Chat, Guid>
    {
        private const string CHAT_ID = "B4AFB0CB-D757-4F17-8B25-AE6C796C5D97";
        private Guid _id;
        private IRoomRepository _roomRepository;
        private static object _createChatLock = new object();
        private static volatile Chat _currentChat;
        
        public virtual Guid Id { get { return _id; } }

        public virtual IEnumerable<Room> Rooms { 
            get { return _roomRepository.GetAll(); } 
        }

        public static Chat Current
        {
            get
            {
                if (_currentChat == null)
                {
                    lock (_createChatLock)
                    {
                        if (_currentChat == null)
                            _currentChat = new Chat(
                                Guid.Parse(CHAT_ID),
                                RepositoryProvider.GetRepository<IRoomRepository>()
                            );
                    }
                }
                return _currentChat;
            }
        }

        internal Chat(Guid id, IRoomRepository roomRepository)
        {
            Check.NotNull(roomRepository, "roomRepository");
            _id             = id;
            _roomRepository = roomRepository;
        }

        public void AddRoom(Room room)
        {
            _roomRepository.Add(room);
        }

        public void BreakRoom(Room room)
        {
            _roomRepository.Remove(room);
        }


        public bool SameIdentityAs(Chat other)
        {
            return other != null && other.Id == Id;
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

            return SameIdentityAs((Chat)obj);
        }
    }
}