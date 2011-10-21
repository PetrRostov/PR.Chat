using System;
using PR.Chat.Infrastructure;

namespace PR.Chat.Test.Common
{
    public class TestEntity : IEntity<TestEntity, Guid>
    {
        private readonly Guid _id;

        public TestEntity()
        {
            _id = Guid.NewGuid();
        }

        public Guid Id
        {
            get { return _id; }
        }

        public bool SameIdentityAs(TestEntity other)
        {
            return other.Id.Equals(_id);
        }
    }
}