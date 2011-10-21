namespace PR.Chat.Infrastructure
{
    public interface IDependencyResolverConfigurator<in T> where T : class, IDependencyResolver
    {
        void Configure(T dependencyResolver);
    }
}