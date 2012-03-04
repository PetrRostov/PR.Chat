using System;
using System.Collections.Generic;
using System.Linq;

namespace PR.Chat.Infrastructure.RightContext
{
    public class RightProxyGenerator : IRightProxyGenerator
    {
        private readonly IProxyBuilder _proxyBuilder;

        public RightProxyGenerator(
            IProxyBuilder proxyBuilder
            )
        {
            Require.NotNull(proxyBuilder, "proxyBuilder");
            _proxyBuilder = proxyBuilder;
        }

        #region Implementation of IRightProxyGenerator

        public virtual T Generate<T>(T obj) where T : class
        {
            var inteceptors = GenerateRightInterceptors(obj.GetType());
            if (inteceptors == null)
                return obj;


            return _proxyBuilder.Build(obj, inteceptors.ToArray());
        }

        public T GenerateFromInterface<T>(T obj) where T : class
        {
            var inteceptors = GenerateRightInterceptors(obj.GetType());
            if (inteceptors == null)
                return obj;


            return _proxyBuilder.BuildInterface(obj, inteceptors.ToArray());
        }

        #endregion

        protected virtual IEnumerable<IInterceptor> GenerateRightInterceptors(Type type)
        {
            var rightContextMemberAttribute = Attribute.GetCustomAttribute(type, typeof (RightContextMemberAttribute));
            if (rightContextMemberAttribute == null)
                yield break;

            var methods = type.GetMethods();
            foreach (var methodInfo in methods)
            {
                var requiredPermission = (RequiredPermissionAttribute)Attribute.GetCustomAttribute(
                    methodInfo, 
                    typeof (RequiredPermissionAttribute)
                );
                if (requiredPermission == null) continue;

                var argumentOptions = new List<ArgumentOptions>();

                var methodParameters = methodInfo.GetParameters();
                for (var i = requiredPermission.Arguments.Length - 1; i >= 0; --i)
                {
                    var argument = requiredPermission.Arguments[i];
                    if (
                        argument != SpecialArgument.This &&
                        argument != SpecialArgument.CurrentUser &&
                        !methodParameters.Any(mp => mp.Name == argument)
                    )
                        throw new ArgumentException(string.Format(
                            "No parameters with name={0} in type={1}, method={2}.",
                            argument,
                            type.FullName,
                            methodInfo.Name
                        ));

                    var parameterPosition = -1;
                    if (argument != SpecialArgument.This && argument != SpecialArgument.CurrentUser)
                        for (var j = methodParameters.Length - 1; j >= 0;  --j)
                            if (methodParameters[j].Name == argument)
                            {
                                parameterPosition = j;
                                break;
                            }

                    argumentOptions.Add(new ArgumentOptions {
                        ParameterPosition   = parameterPosition,
                        ArgumentPosition    = i,
                        IsMethodOwner       = argument == SpecialArgument.This,
                        IsExecutor          = argument == SpecialArgument.CurrentUser 
                    });
                }

                var interceptorOptions = new RightContextInterceptorOptions
                {
                    Arguments           = argumentOptions,
                    Permission          = requiredPermission.RequiredPermission,
                    RuleHolderOptions   = new RuleHolderOptions {
                        RuleHolder                  = requiredPermission.RuleHolder,
                        RuleHolderArgumentPosition  = requiredPermission.RuleHolderArgumentPosition
                    }
                };

                yield return CreateInterceptor(interceptorOptions);
            }
        }

        protected virtual IInterceptor CreateInterceptor(RightContextInterceptorOptions options)
        {
            return new RightContextInterceptor(options);
        }
    }
}