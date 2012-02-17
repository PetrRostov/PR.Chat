using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    internal static class RepositoryProvider
    {
        internal static T GetRepository<T>()
        {
            return IoC.Resolve<T>();
        }
    }
}