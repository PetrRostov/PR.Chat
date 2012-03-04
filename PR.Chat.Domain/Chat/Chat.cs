using System;
using System.Collections.Generic;
using PR.Chat.Infrastructure;
using PR.Chat.Infrastructure.RightContext;

namespace PR.Chat.Domain
{
    public class Chat : IEntity<Chat, Guid>
    {
        private const string CHAT_ID = "B4AFB0CB-D757-4F17-8B25-AE6C796C5D97";
        private static readonly object _createChatLock = new object();
        private static volatile Chat _currentChat;
        private readonly Guid _id;
        private readonly IRoomRepository _roomRepository;

        internal Chat(Guid id, IRoomRepository roomRepository)
        {
            Require.NotNull(roomRepository, "roomRepository");
            _id             = id;
            _roomRepository = roomRepository;
        }

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

        #region IEntity<Chat,Guid> Members

        public virtual Guid Id { get { return _id; } }

        public bool SameIdentityAs(Chat other)
        {
            return other != null && other.Id == Id;
        }

        #endregion

        [RequiredPermission(ChatRightRules.AddRoomPermission, SpecialArgument.This, SpecialArgument.CurrentUser, "room")]
        public virtual void AddRoom(Room room)
        {
            _roomRepository.Add(room);
        }

        public void BreakRoom(Room room)
        {
            _roomRepository.Remove(room);
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