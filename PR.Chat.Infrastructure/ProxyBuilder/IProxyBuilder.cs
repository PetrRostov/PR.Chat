namespace PR.Chat.Infrastructure
{
    public interface IProxyBuilder
    {
        T Build<T>(IInterceptor[] interceptors) where T : class;

        T Build<T>(T targetObj, IInterceptor[] interceptors) where T : class;

        T BuildInterface<T>(T targetObj, IInterceptor[] interceptors) where T : class;
    }
}