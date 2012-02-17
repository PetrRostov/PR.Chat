using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Castle.DynamicProxy.Generators;

namespace PR.Chat.Infrastructure.Castle
{
    public class ProxyBuilder : IProxyBuilder
    {
        private readonly ProxyGenerator _proxyGenerator;

        public ProxyBuilder()
            : this(new ProxyGenerator(new PersistentProxyBuilder()))
        {
            
        }

        public ProxyBuilder(ProxyGenerator proxyGenerator)
        {
            _proxyGenerator = proxyGenerator;
        }

        #region Implementation of IProxyBuilder

        public TClass Build<TClass>(IInterceptor[] interceptors) where TClass : class
        {
            var castleInterceptors = interceptors
                .Select(interceptor => new ChatInterceptorAdapter(interceptor))
                .Cast<global::Castle.DynamicProxy.IInterceptor>()
                .ToArray();

            return _proxyGenerator.CreateClassProxy<TClass>(castleInterceptors);
        }

        public T Build<T>(T targetObj, IInterceptor[] interceptors) where T : class
        {
            var castleInterceptors = interceptors
                .Select(interceptor => new ChatInterceptorAdapter(interceptor))
                .Cast<global::Castle.DynamicProxy.IInterceptor>()
                .ToArray();

            return _proxyGenerator.CreateClassProxyWithTarget(targetObj, castleInterceptors);
        }

        #endregion
    }

    public class ProxyGenerationHook : IProxyGenerationHook
    {

        #region Implementation of IProxyGenerationHook

        public void MethodsInspected()
        {
            throw new NotImplementedException();
        }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
            throw new NotImplementedException();
        }

        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}