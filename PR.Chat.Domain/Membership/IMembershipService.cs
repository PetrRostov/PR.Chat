namespace PR.Chat.Domain
{
    public interface IMembershipService
    {
        Membership Register(User user, string login, string password);

        Membership Register(string login, string password);
    }
}