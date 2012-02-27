using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Tests
{
    [TestFixture, Category(TestCategory.Infrastructure)]
    public class RightProxyGeneratorFixtures
    {
        private TestRightProxyGenerator _testRightProxyGenerator;
        private Mock<IProxyBuilder> _proxyBuilder;

        [SetUp]
        public void Init()
        {
            _proxyBuilder = new Mock<IProxyBuilder>();
            _testRightProxyGenerator = new TestRightProxyGenerator(_proxyBuilder.Object);
        }

        [Test]
        public void GenerateRightInterceptors_should_return_right_result()
        {
            var interceptors = _testRightProxyGenerator
                .GenerateRightInterceptorsPublic(typeof(ObjectOneForRightContext))
                .Cast<TestInterceptor>()
                .ToArray();

            Assert.AreEqual(interceptors.Length, 2);

            var interceptorForUpdatePermission = interceptors.FirstOrDefault(i => i.Options.Permission == "Update");
            Assert.NotNull(interceptorForUpdatePermission);

            var optionsArguments = interceptorForUpdatePermission.Options.Arguments.ToArray();
            Assert.NotNull(optionsArguments);
            Assert.AreEqual(optionsArguments.Length, 3);

            var firstArgument = optionsArguments.FirstOrDefault(a => a.ArgumentPosition == 0);
            Assert.NotNull(firstArgument);
            Assert.IsFalse(firstArgument.IsExecutor);
            Assert.IsFalse(firstArgument.IsMethodOwner);
            Assert.AreEqual(firstArgument.ParameterPosition, 1);

            var secondArgument = optionsArguments.FirstOrDefault(a => a.ArgumentPosition == 1);
            Assert.NotNull(secondArgument);
            Assert.IsFalse(secondArgument.IsExecutor);
            Assert.IsFalse(secondArgument.IsMethodOwner);
            Assert.AreEqual(secondArgument.ParameterPosition, 0);

            var thirdArgument = optionsArguments.FirstOrDefault(a => a.ArgumentPosition == 2);
            Assert.NotNull(thirdArgument);
            Assert.IsFalse(thirdArgument.IsExecutor);
            Assert.IsTrue(thirdArgument.IsMethodOwner);


            var interceptorForReceivePermission = interceptors.FirstOrDefault(i => i.Options.Permission == "Receive");
            Assert.NotNull(interceptorForReceivePermission);

            optionsArguments = interceptorForReceivePermission.Options.Arguments.ToArray();
            Assert.NotNull(optionsArguments);
            Assert.AreEqual(optionsArguments.Length, 2);

            firstArgument = optionsArguments.FirstOrDefault(a => a.ArgumentPosition == 0);
            Assert.NotNull(firstArgument);
            Assert.IsFalse(firstArgument.IsExecutor);
            Assert.IsTrue(firstArgument.IsMethodOwner);

            secondArgument = optionsArguments.FirstOrDefault(a => a.ArgumentPosition == 1);
            Assert.NotNull(secondArgument);
            Assert.IsFalse(secondArgument.IsExecutor);
            Assert.IsFalse(secondArgument.IsMethodOwner);
            Assert.AreEqual(secondArgument.ParameterPosition, 0);
        }

        [Test]
        public void GenerateRightInterceptors_should_throw_ArgumentException_if_parameter_name_not_found_in_method()
        {
            Assert.Throws<ArgumentException>(() => 
                _testRightProxyGenerator
                    .GenerateRightInterceptorsPublic(typeof(ObjectWithWrongRequiredPermissionAttributeInit))
                    .ToArray()
            );

        }

        [Test]
        public void GenerateRightInterceptors_should_return_empty_result_if_class_have_not_RightContextMemberAttribute()
        {
            var result = _testRightProxyGenerator.GenerateRightInterceptorsPublic(typeof(ObjectTwoForRightContext));
            CollectionAssert.IsEmpty(result);
        }
    }
}