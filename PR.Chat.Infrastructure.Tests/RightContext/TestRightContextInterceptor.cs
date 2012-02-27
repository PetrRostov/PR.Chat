using System;
using System.Collections.Generic;
using PR.Chat.Infrastructure.RightContext;

namespace PR.Chat.Infrastructure.Tests
{
    public class TestRightContextInterceptor : RightContextInterceptor
    {
        private readonly IRightRuleRepository _rightRuleRepository;

        public TestRightContextInterceptor(
            RightContextInterceptorOptions options, 
            IRightRuleRepository rightRuleRepository
            ) : base(options)
        {
            _rightRuleRepository = rightRuleRepository;
        }

        public IEntity<Guid> GetRuleHolderPublic(IInvocation invocation)
        {
            return GetRuleHolder(invocation);
        }

        public IEnumerable<RightRule> GetRulesPublic(IEntity<Guid> ruleHolder)
        {
            return GetRules(ruleHolder);
        }

        public object[] GetExpressionArgumentsPublic(IInvocation invocation)
        {
            return GetExpressionArguments(invocation);
        }

        protected override IRightRuleRepository GetRuleRepository()
        {
            return _rightRuleRepository;
        }
    }
}