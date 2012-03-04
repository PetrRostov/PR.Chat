using System;

namespace PR.Chat.Infrastructure
{
    public class Event : IEntity<Guid>
    {
        #region Implementation of IEntity<out Guid>

        public Guid Id
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}