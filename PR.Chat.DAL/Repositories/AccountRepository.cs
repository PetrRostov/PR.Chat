using System.Linq;
using PR.Chat.Core.BusinessObjects;
using PR.Chat.Core.Repositories;
using PR.Chat.DAL.EF;

namespace PR.Chat.DAL.Repositories
{
    public class AccountRepository : BaseRepository<IAccount, Account>, IAccountRepository
    {
        public AccountRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public AccountRepository(IDatabase database) : base(database)
        {
        }

        public IAccount GetByLogin(string login)
        {
            return GetSource().FirstOrDefault(acc => acc.Login == login);
        }

        public IAccount GetByLoginAndPassword(string login, string password)
        {
            return GetSource().FirstOrDefault(acc => acc.Login == login);

            //return Database.GetSource<IAccount>().FirstOrDefault(acc => acc.Login == login && acc);
        }
    }
}