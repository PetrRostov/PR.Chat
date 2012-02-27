using NHibernate;

namespace PR.Chat.Infrastructure.Data.NH
{
    public class SessionFactoryFactory : ISessionFactoryFactory
    {
        private readonly INHibernateDatabaseConfigurator _configurator;
        private ISessionFactory _sessionFactory;

        public SessionFactoryFactory(INHibernateDatabaseConfigurator configurator)
        {
            Require.NotNull(configurator, "configurator");
            _configurator = configurator;
        }

        public ISessionFactory Create()
        {
            return _sessionFactory ?? (_sessionFactory = _configurator.GetConfiguration().BuildSessionFactory());
        }
    }
}