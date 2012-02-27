using Moq;
using PR.Chat.Infrastructure.RightContext;

namespace PR.Chat.Infrastructure.Tests
{
    public class TestInterceptor : IInterceptor
    {
        public RightContextInterceptorOptions Options { get; set; }

        public TestInterceptor(RightContextInterceptorOptions options)
        {
            Options = options;
        }

        #region Implementation of IInterceptor

        public void PreProceed(IInvocation invocation)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}