using PR.Chat.Core.BusinessObjects;

namespace PR.Chat.Core.Services
{
    public interface IAccountService
    {
        AuthResult Auth(string login, string password);

        NickCreateResult CreateNick(string name);
    }

}