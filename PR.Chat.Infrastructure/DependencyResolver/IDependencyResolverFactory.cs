namespace PR.Chat.Infrastructure
{
    public interface IDependencyResolverFactory
    {
        IDependencyResolver Create();
    }
}