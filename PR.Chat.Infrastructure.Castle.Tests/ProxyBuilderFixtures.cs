using System;
using Moq;
using NUnit.Framework;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Castle.Tests
{
    [TestFixture, Category(TestCategory.Infrastructure)]
    public class ProxyBuilderFixtures
    {
        private ProxyBuilder _proxyBuilder;
        private Mock<IInterceptor> _interceptor;
        private IInvocation _lastInvocation;

        [SetUp]
        public void Init()
        {
            _proxyBuilder = new ProxyBuilder();

            _interceptor = new Mock<IInterceptor>();
            _interceptor
                .Setup(i => i.PreProceed(It.IsAny<IInvocation>()))
                .Callback((IInvocation i) => _lastInvocation = i)
                .Verifiable();
        }

        [Test]
        public void Before_call_proxy_method_should_call_PreProceed_IInterceptor_method()
        {
            var classAProxy = _proxyBuilder.Build<ClassA>(new[] { _interceptor.Object });

            classAProxy.MethodForIntercept1("1", Guid.Empty);
            _interceptor.Verify();

            classAProxy.MethodForIntercept2(1, "4");
            _interceptor.Verify();
        }

        [Test]
        public void PreProceed_should_have_right_IInvocation_argument()
        {
            var classA = new ClassA();
            var classAProxy = _proxyBuilder.Build<ClassA>(new[] { _interceptor.Object });

            var resultProxy = classAProxy.MethodForIntercept1("1", Guid.Empty);
            var result = classA.MethodForIntercept1("1", Guid.Empty);
            
            Assert.NotNull(_lastInvocation);
            Assert.AreEqual(_lastInvocation.Arguments[0], "1");
            Assert.AreEqual(_lastInvocation.Arguments[1], Guid.Empty);
            Assert.AreEqual(_lastInvocation.Method.Name, "MethodForIntercept1");
            Assert.AreEqual(_lastInvocation.TargetType, typeof(ClassA));
            Assert.AreEqual(result, resultProxy);
        }

        [Test]
        public void BuildWithTarget_should_use_target_object()
        {
            var classA = new ClassA("innerString");
            var classAProxy = _proxyBuilder.Build(classA, new[] {_interceptor.Object});

            Assert.AreEqual(classAProxy.GetInnerString(), classA.GetInnerString());

        }
    }
}