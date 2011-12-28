namespace PR.Chat.Application
{
    public interface IMembershipService
    {
        EnterResult EnterAsUnregistered(string nickName);

        EnterResult EnterAsRegisteredByNick(string nickName, string password);

        EnterResult EnterAsRegistered(string membershipLogin, string password);

        RegisterResult Register(
            string email,
            string password,
            string repeatPassword
        );

        RegisterResult Register(
            string email, 
            string password, 
            string repeatPassword,
            string nickName 
        );
    }
}