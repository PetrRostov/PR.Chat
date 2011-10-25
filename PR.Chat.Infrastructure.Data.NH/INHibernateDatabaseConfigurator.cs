using NHibernate.Cfg;

namespace PR.Chat.Infrastructure.Data.NH
{
    public interface INHibernateDatabaseConfigurator
    {
        Configuration GetConfiguration();
    }
}