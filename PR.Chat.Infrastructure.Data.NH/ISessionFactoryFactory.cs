using NHibernate;

namespace PR.Chat.Infrastructure.Data.NH
{
    public interface ISessionFactoryFactory
    {
        ISessionFactory Create();
    }
}