namespace PR.Chat.Infrastructure.Castle
{
    public class ChatInterceptorAdapter : global::Castle.DynamicProxy.StandardInterceptor
    {
        private readonly IInterceptor _interceptor;

        public ChatInterceptorAdapter(IInterceptor interceptor)
        {
            Check.NotNull(interceptor, "interceptor");
            _interceptor = interceptor;
        }

        protected override void PreProceed(global::Castle.DynamicProxy.IInvocation invocation)
        {
            _interceptor.PreProceed(new CastleInvocationAdapter(invocation));
            base.PreProceed(invocation);
        }
    }
}