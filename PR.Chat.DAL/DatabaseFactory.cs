using PR.Chat.DAL.EF;

namespace PR.Chat.DAL
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly IConnectionStringProvider _connectionStringProvider;
        
        public DatabaseFactory()
        {
            
        }

        public DatabaseFactory(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public IDatabase Create()
        {
            return new ChatDB(_connectionStringProvider.GetConnectionString());
        }
    }
}