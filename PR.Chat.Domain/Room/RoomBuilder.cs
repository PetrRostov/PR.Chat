using System;

namespace PR.Chat.Domain
{
    public class RoomBuilder : IRoomBuilder
    {
        public Room BuildRoom(string name, string description, bool isTemporary)
        {
            return new Room(Guid.NewGuid(), name, description, isTemporary);
        }
    }
}