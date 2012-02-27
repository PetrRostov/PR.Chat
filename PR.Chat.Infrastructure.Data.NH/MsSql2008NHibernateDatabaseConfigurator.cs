using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace PR.Chat.Infrastructure.Data.NH
{
    public class MsSql2008NHibernateDatabaseConfigurator : INHibernateDatabaseConfigurator
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public MsSql2008NHibernateDatabaseConfigurator(IConnectionStringProvider connectionStringProvider)
        {
            Require.NotNull(connectionStringProvider, "connectionStringProvider");
            _connectionStringProvider = connectionStringProvider;
        }

        public Configuration GetConfiguration()
        {
            return new Configuration()
                .SetProperty(Environment.ConnectionString, _connectionStringProvider.GetConnectionString())
                .SetProperty(Environment.ShowSql, true.ToString())
                .SetProperty(Environment.Dialect, typeof (MsSql2008Dialect).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionDriver, typeof (Sql2008ClientDriver).AssemblyQualifiedName)
                .SetProperty(Environment.ProxyFactoryFactoryClass, typeof (DefaultProxyFactoryFactory).AssemblyQualifiedName)
                .AddAssembly(GetType().Assembly);
        }
    }
}