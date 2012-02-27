using System;
using System.Configuration;

namespace PR.Chat.Infrastructure.Data.NH
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly string _connectionStringName;

        public ConnectionStringProvider(string connectionStringName)
        {
            Require.NotNullOrEmpty(connectionStringName, "connectionStringName");
            
            _connectionStringName = connectionStringName;
        }

        public string GetConnectionString()
        {
            if (ConfigurationManager.ConnectionStrings[_connectionStringName] == null)
                throw new ConnectionStringNotFoundException();

            return ConfigurationManager.ConnectionStrings[_connectionStringName].ConnectionString;
        }
    }
}