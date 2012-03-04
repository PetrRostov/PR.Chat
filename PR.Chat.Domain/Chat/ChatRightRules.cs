using System;
using System.Linq.Expressions;
using PR.Chat.Infrastructure.RightContext;

namespace PR.Chat.Domain
{
    public static class ChatRightRules
    {
        internal const string AddRoomPermission = "AddRoomPermission";

        public static RightRule DeniedAddingRoomForUser(this Chat c,  User u, DateTime expired)
        {
            Expression<Func<Chat, Nick, Room, bool>> expression = (chat, executor, room) => u.Id != executor.User.Id;
            return new RightRule(
                Guid.NewGuid(),
                c.Id,
                AddRoomPermission,
                expression,
                expired
            );
        }

        public static RightRule DeniedAddingRoomForNick(this Chat c, Nick n, DateTime expired)
        {
            Expression<Func<Chat, Nick, Room, bool>> expression = (chat, executor, room) => n.Id != executor.Id;
            return new RightRule(
                Guid.NewGuid(),
                c.Id,
                AddRoomPermission,
                expression,
                expired
            );
        }

        public static RightRule GrantAddingRoomForAll(this Chat c, DateTime expired)
        {
            Expression<Func<Chat, Nick, Room, bool>> expression = (chat, executor, room) => true;
            return new RightRule(
                Guid.NewGuid(),
                c.Id,
                AddRoomPermission,
                expression,
                expired
            );
        }
    }
}