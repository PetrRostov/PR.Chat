using System;

namespace PR.Chat.Domain.Tests
{
    public class UserWithSetters : User
    {
        private Guid _id;

        public void SetId(Guid id)
        {
            _id = id;
        }

        public override Guid Id
        {
            get
            {
                return _id;
            }
        }
    }
}