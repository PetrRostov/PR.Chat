using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using PR.Chat.Infrastructure;
using PR.Chat.Infrastructure.Enumeration;

namespace PR.Chat.Domain
{
    public static class Permission
    {
        /// <summary>
        /// Check delegate is Func&lt;User, Message, bool&gt;
        /// </summary>
        public static readonly Permission<Expression<Func<Nick, Message, bool>>> ReceiveMessage =
            new Permission<Expression<Func<Nick, Message, bool>>>(@"RECEIVE_MESSAGE");

        /// <summary>
        /// Check delegate is Func&lt;User, Message, bool&gt;
        /// </summary>
        public static readonly Permission<Expression<Func<Nick, Message, bool>>> SendMessage =
            new Permission<Expression<Func<Nick, Message, bool>>>(@"SEND_MESSAGE");

        /// <summary>
        /// Check delegate is Func&lt;Nick, Room, bool&gt;
        /// </summary>
        public static readonly Permission<Expression<Func<Nick, Room, bool>>> ChangeRoomDescription =
            new Permission<Expression<Func<Nick, Room, bool>>>(@"CHANGE_ROOM_DESCRIPTION");

        public static IEnumerable<IPermission> GetAll()
        {
            var type = typeof(Permission);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields
                .Select(info => info.GetValue(null))
                .OfType<IPermission>();
        }

        public static IPermission GetByName(string name)
        {
            var permission = GetAll().FirstOrDefault(p => p.Name.Equals(name));
            if (permission == null)
                throw new PermissionNotFoundException(string.Format(
                    "Permission with Name={0} not found",
                    name
                ));

            return permission;
        }
    }
}