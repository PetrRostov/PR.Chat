using System.Collections.Generic;
using System.Diagnostics;

namespace PR.Chat.Infrastructure
{
    public static class IoC
    {
        private static IDependencyResolver _resolver;

        public static void InitializeWith(IDependencyResolver dependencyResolver)
        {
            Require.NotNull(dependencyResolver, "dependencyResolver");
            _resolver = dependencyResolver;
        }

        public static void InitializeWith(IDependencyResolverFactory dependencyResolverFactory)
        {
            Require.NotNull(dependencyResolverFactory, "dependencyResolverFactory");
            _resolver = dependencyResolverFactory.Create();
        }

        [DebuggerStepThrough]
        public static void Register<T>(T instance)
        {
            _resolver.Register(instance);
        }

        [DebuggerStepThrough]
        public static T Resolve<T>()
        {
            return _resolver.Resolve<T>();
        }

        [DebuggerStepThrough]
        public static T Resolve<T>(string name)
        {
            return _resolver.Resolve<T>(name);
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> ResolveAll<T>()
        {
            return _resolver.ResolveAll<T>();
        }

        [DebuggerStepThrough]
        public static void Close()
        {
            _resolver.Dispose();
        }
    }
}