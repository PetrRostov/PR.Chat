namespace PR.Chat.Domain
{
    public interface IUserFactory
    {
        User CreateUnregistered();
        User CreateRegistered(string name, string password);
    }
}