using System;

namespace PR.Chat.Infrastructure
{
    public class BaseAggregateRoot : IEntity<Guid>
    {
        #region Implementation of IEntity<out Guid>

        private readonly Guid _id;

        public BaseAggregateRoot(Guid id)
        {
            _id = id;
        }

        public Guid Id
        {
            get { return _id; }
        }

        #endregion
    }
}