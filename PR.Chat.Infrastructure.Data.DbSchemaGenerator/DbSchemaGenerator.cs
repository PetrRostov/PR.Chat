using System;
using NHibernate.Tool.hbm2ddl;
using PR.Chat.Infrastructure.Data.NH;

namespace PR.Chat.Infrastructure.Data.DbSchemaGenerator
{
    public static class DbSchemaGenerator
    {
        public static void Main(string[] args)
        {
            var configurator = new MsSql2008NHibernateDatabaseConfigurator(
                new ConnectionStringProvider()
            );
            
            new SchemaExport(configurator.GetConfiguration())
                .Create(true, false);

        }
    }

    internal class ConnectionStringProvider : IConnectionStringProvider
    {
        public string GetConnectionString()
        {
            return string.Empty;
        }
    }
}