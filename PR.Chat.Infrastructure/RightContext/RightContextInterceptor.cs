using System;
using System.Collections.Generic;
using System.Linq;

namespace PR.Chat.Infrastructure.RightContext
{
    public class RightContextInterceptor : IInterceptor
    {
        protected readonly RightContextInterceptorOptions Options;

        public RightContextInterceptor(RightContextInterceptorOptions options)
        {
            Require.NotNull(options, "options");
            Options = options;
        }

        #region Implementation of IInterceptor

        public void PreProceed(IInvocation invocation)
        {

            var ruleHolderEntity    = GetRuleHolder(invocation);
            var ruleArguments       = GetExpressionArguments(invocation);
            var rules               = GetRules(ruleHolderEntity);
            bool hasRight;
            foreach (var rightRule in rules)
            {
                hasRight = (bool) rightRule.CheckExpression.Compile().DynamicInvoke(ruleArguments);
                if (!hasRight)
                    throw new RightException(string.Format(
                        "Access denied. Required permission «{0}» Type={1}, method={2}",
                        Options.Permission,
                        invocation.TargetType.FullName,
                        invocation.Method.Name
                    ));
            }

        }

        protected virtual IEnumerable<RightRule> GetRules(IEntity<Guid> ruleHolder)
        {
            return GetRuleRepository().Get(ruleHolder.Id, Options.Permission);
        }

        protected virtual IEntity<Guid> GetRuleHolder(IInvocation invocation)
        {
            object ruleHolder = null;

            switch (Options.RuleHolderOptions.RuleHolder)
            {
                case RuleHolder.MethodOwner:
                    ruleHolder = invocation.InvocationTarget;
                    break;
                case RuleHolder.Executor:
                    throw new NotImplementedException();
                    break;
                case RuleHolder.Argument:
                    var position = (int)Options.RuleHolderOptions.RuleHolderArgumentPosition;
                    ruleHolder = invocation.Arguments[position];
                    break;
            }

            var ruleHolderEntity = ruleHolder as IEntity<Guid>;
            if (ruleHolderEntity == null)
                throw new ArgumentException(string.Format(
                    "RightRule holder should be implemented from IEntity<Guid>. Type={0}, method={1}.",
                    invocation.InvocationTarget.GetType(),
                    invocation.Method.Name
                ));

            return ruleHolderEntity;
        }

        protected virtual object[] GetExpressionArguments(IInvocation invocation)
        {
            object[] ruleArguments = new object[Options.Arguments.Count()];
            foreach (var argumentOption in Options.Arguments)
            {
                var position = argumentOption.ArgumentPosition;
                if (argumentOption.IsMethodOwner)
                    ruleArguments[position] = invocation.InvocationTarget;
                else if (argumentOption.IsExecutor)
                    throw new NotImplementedException();
                else
                    ruleArguments[position] = invocation.Arguments[argumentOption.ParameterPosition];
            }
            return ruleArguments;
        }

        protected  virtual IRightRuleRepository GetRuleRepository()
        {
            return IoC.Resolve<IRightRuleRepository>();
        }

        #endregion
    }
}