using PR.Chat.Core.BusinessObjects;

namespace PR.Chat.Core.Repositories
{
    public interface IAccountRepository : IRepository<IAccount>
    {
        IAccount GetByLogin(string login);

        IAccount GetByLoginAndPassword(string login, string password);
    }
}