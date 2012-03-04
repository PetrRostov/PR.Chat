namespace PR.Chat.Domain
{
    public class UserFactory : IUserFactory
    {
        #region IUserFactory Members

        public User CreateUnregistered()
        {
            return new User(
                false
            );
        }

        public User CreateRegistered()
        {
            return new User(true);
        }

        #endregion
    }
}