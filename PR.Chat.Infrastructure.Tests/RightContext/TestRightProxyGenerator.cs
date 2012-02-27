using System;
using System.Collections.Generic;
using PR.Chat.Infrastructure.RightContext;

namespace PR.Chat.Infrastructure.Tests
{
    public class TestRightProxyGenerator : RightProxyGenerator
    {
        public TestRightProxyGenerator(IProxyBuilder proxyBuilder) : base(proxyBuilder)
        {
        }

        public IEnumerable<IInterceptor> GenerateRightInterceptorsPublic(Type type)
        {
            return GenerateRightInterceptors(type);
        }

        protected override IInterceptor CreateInterceptor(RightContextInterceptorOptions options)
        {
            return new TestInterceptor(options);
        }
    }
}