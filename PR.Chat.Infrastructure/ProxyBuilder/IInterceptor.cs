namespace PR.Chat.Infrastructure
{
    public interface IInterceptor
    {
        void PreProceed(IInvocation invocation);
    }
}