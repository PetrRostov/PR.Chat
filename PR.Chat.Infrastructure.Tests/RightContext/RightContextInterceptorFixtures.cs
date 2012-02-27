using System;
using System.Linq.Expressions;
using Moq;
using NUnit.Framework;
using PR.Chat.Infrastructure.RightContext;
using PR.Chat.Test.Common;

namespace PR.Chat.Infrastructure.Tests
{
    [TestFixture, Category(TestCategory.Infrastructure)]
    public class RightContextInterceptorFixtures
    {
        private Mock<IRightRuleRepository> _rightRuleRepository;
        private RightRule _rightRuleForUpdate;
        private RightRule _rightRuleForReceive;
        private RightRule _rightRuleWithUnknownExpression;
        private TestRightContextInterceptor _rightContextInterceptor;
        private RightContextInterceptorOptions _updateInterceptorOptions;

        [SetUp]
        public void Init()
        {

            _rightRuleForUpdate = new RightRule(
                Guid.NewGuid(), 
                Guid.NewGuid(),
                "Update",
                (Expression<Func<ObjectOneForRightContext, string, int, bool>>)((str1, str, i) => true),
                DateTime.Now.AddYears(1)
            );
            _rightRuleForReceive = new RightRule(
                Guid.NewGuid(), 
                Guid.NewGuid(),
                "Receive",
                (Expression<Func<string, int, bool>>)((str, i) => false),
                DateTime.Now.AddYears(1)
            );
            _rightRuleWithUnknownExpression = new RightRule(
                Guid.NewGuid(), 
                Guid.NewGuid(),
                "Unknown",
                (Expression<Func<string, int, bool>>)((str, i) => true),
                DateTime.Now.AddYears(1)
            );


             _rightRuleRepository = new Mock<IRightRuleRepository>();
            _rightRuleRepository
                .Setup(r => r.Get(It.IsAny<Guid>(), "Update"))
                .Returns(() => new[] {_rightRuleForUpdate});

            _rightRuleRepository
                .Setup(r => r.Get(It.IsAny<Guid>(), "Receive"))
                .Returns(() => new[] { _rightRuleForReceive });

            _rightRuleRepository
                .Setup(r => r.Get(It.IsAny<Guid>(), "Unknown"))
                .Returns(() => new[] { _rightRuleWithUnknownExpression });

            
        }

        [Test]
        public void GetRuleHolder_should_work_if_RuleHolder_is_methodOwner()
        {
            
            var entity = new Mock<IEntity<Guid>>();
            entity.SetupGet(e => e.Id).Returns(Guid.NewGuid());

            var invocation = new Mock<IInvocation>();
            invocation.SetupGet(i => i.InvocationTarget).Returns(entity.Object);


            var interceptor = new TestRightContextInterceptor(new RightContextInterceptorOptions {
                    RuleHolderOptions = new RuleHolderOptions {
                        RuleHolder                  = RuleHolder.MethodOwner,
                        RuleHolderArgumentPosition  = ArgumentPosition.WithoutPosition
                    }
                }, 
                _rightRuleRepository.Object
            );
            var ruleHolder = interceptor.GetRuleHolderPublic(invocation.Object);

            Assert.AreSame(entity.Object, ruleHolder);

        }

        [Test]
        public void GetRuleHolder_should_work_if_RuleHolder_is_argument()
        {

            var entity = new Mock<IEntity<Guid>>();
            entity.SetupGet(e => e.Id).Returns(Guid.NewGuid());

            var invocation = new Mock<IInvocation>();
            var arguments = new object[] {1, entity.Object};
            invocation.SetupGet(i => i.Arguments).Returns(arguments);


            var interceptor = new TestRightContextInterceptor(
                new RightContextInterceptorOptions{
                    RuleHolderOptions = new RuleHolderOptions {
                        RuleHolder = RuleHolder.Argument,
                        RuleHolderArgumentPosition = ArgumentPosition.Second
                    }
                },
                _rightRuleRepository.Object
            );
            var ruleHolder = interceptor.GetRuleHolderPublic(invocation.Object);

            Assert.AreSame(entity.Object, ruleHolder);
        }

        [Test]
        public void GetRuleHolder_should_throw_ArgumentException_if_ruleHolder_not_IEntity()
        {
            var invocation = new Mock<IInvocation>();
            invocation.SetupGet(i => i.InvocationTarget).Returns(this);
            invocation.SetupGet(i => i.Method).Returns(
                GetType().GetMethod("GetRuleHolder_should_throw_ArgumentException_if_ruleHolder_not_IEntity")
            );

            var arguments = new object[] { 1, "str" };
            invocation.SetupGet(i => i.Arguments).Returns(arguments);


            var interceptor = new TestRightContextInterceptor(
                new RightContextInterceptorOptions
                {
                    RuleHolderOptions = new RuleHolderOptions
                    {
                        RuleHolder = RuleHolder.Argument,
                        RuleHolderArgumentPosition = ArgumentPosition.Second
                    }
                },
                _rightRuleRepository.Object
            );

            Assert.Throws<ArgumentException>(
                () => interceptor.GetRuleHolderPublic(invocation.Object)
            );
        }

        [Test]
        public void GetRules_use_IRightRuleRepository()
        {
            var guid = Guid.NewGuid();
            _rightRuleRepository
                .Setup(r => r.Get(guid, "Update"))
                .Returns(new[] { _rightRuleForUpdate })
                .Verifiable();

            var entity = new Mock<IEntity<Guid>>();
            entity.SetupGet(e => e.Id).Returns(guid);

            var interceptor = new TestRightContextInterceptor(
                new RightContextInterceptorOptions
                {
                    Permission = "Update",
                    RuleHolderOptions = new RuleHolderOptions
                    {
                        RuleHolder = RuleHolder.Argument,
                        RuleHolderArgumentPosition = ArgumentPosition.Second
                    }
                },
                _rightRuleRepository.Object
            );

            var rules = interceptor.GetRulesPublic(entity.Object);

            _rightRuleRepository.Verify(r => r.Get(guid, "Update"));
        }

        [Test]
        public void GetExpressionArguments_should_work_correct()
        {
            var updateInterceptorOptions = new RightContextInterceptorOptions
            {
                Permission = _rightRuleForUpdate.Permission,
                RuleHolderOptions = new RuleHolderOptions
                {
                    RuleHolder = RuleHolder.MethodOwner,
                    RuleHolderArgumentPosition = ArgumentPosition.WithoutPosition
                },
                Arguments = new[] {
                    new ArgumentOptions {
                        ArgumentPosition    = 0,
                        IsMethodOwner       = true,
                        IsExecutor          = false,
                        ParameterPosition   = -1
                    },
                    new ArgumentOptions {
                        ArgumentPosition    = 2,
                        IsMethodOwner       = false,
                        IsExecutor          = false,
                        ParameterPosition   = 1
                    },
                    new ArgumentOptions {
                        ArgumentPosition    = 1,
                        IsMethodOwner       = false,
                        IsExecutor          = false,
                        ParameterPosition   = 0
                    }
                }
            };

            var objectOne       = new ObjectOneForRightContext();
            var methodArguments = new object[] {"string", 34, DateTime.Now};
            var invocation      = new Mock<IInvocation>();
            
            invocation.SetupGet(i => i.InvocationTarget).Returns(objectOne);
            invocation.SetupGet(i => i.Method).Returns(objectOne.GetType().GetMethod("Update"));
            invocation.SetupGet(i => i.Arguments).Returns(methodArguments);

            var interceptor = new TestRightContextInterceptor(updateInterceptorOptions, _rightRuleRepository.Object);
            var arguments = interceptor.GetExpressionArgumentsPublic(invocation.Object);


            Assert.NotNull(arguments);
            Assert.AreEqual(arguments.Length, 3);
            Assert.AreSame(arguments[0], objectOne);
            Assert.AreEqual(arguments[1], methodArguments[0]);
            Assert.AreEqual(arguments[2], methodArguments[1]);
        }
    }
}