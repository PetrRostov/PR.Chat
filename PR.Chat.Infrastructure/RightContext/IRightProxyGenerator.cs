namespace PR.Chat.Infrastructure.RightContext
{
    public interface IRightProxyGenerator
    {
        T Generate<T>(T obj) where T : class;

        T GenerateFromInterface<T>(T obj) where T : class;
    }
}