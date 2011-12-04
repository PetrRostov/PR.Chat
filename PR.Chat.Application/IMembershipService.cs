using PR.Chat.Application.DTO;

namespace PR.Chat.Application
{
    public interface IMembershipService
    {
        EnterResult EnterAsUnregistered(string nickName);

        EnterResult EnterAsRegistered(string email, string password);

        RegisterResult Register(
            string email, 
            string password, 
            string repeatPassword,
            string nickName
        );


    }
}