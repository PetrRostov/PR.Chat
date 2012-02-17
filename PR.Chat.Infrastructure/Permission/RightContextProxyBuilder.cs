namespace PR.Chat.Infrastructure
{
    public class RightContextProxyBuilder : IProxyBuilder
    {
        private readonly IProxyBuilder _proxyBuilder;

        public RightContextProxyBuilder(IProxyBuilder proxyBuilder)
        {
            Check.NotNull(proxyBuilder, "proxyBuilder");

            _proxyBuilder = proxyBuilder;
        }

        #region Implementation of IProxyBuilder

        public T Build<T>(IInterceptor[] interceptors) where T : class
        {
            throw new System.NotImplementedException();
        }

        public T Build<T>(T targetObj, IInterceptor[] interceptors) where T : class
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}