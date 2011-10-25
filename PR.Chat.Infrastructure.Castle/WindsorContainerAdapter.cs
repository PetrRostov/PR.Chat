using System;
using System.Collections.Generic;
using System.Diagnostics;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace PR.Chat.Infrastructure.Castle
{
    public class WindsorContainerAdapter : IDependencyResolver
    {
        private readonly IWindsorContainer _windsorContainer;

        public WindsorContainerAdapter(IWindsorContainer windsorContainer)
        {
            Check.NotNull(windsorContainer, "windsorContainer");
            _windsorContainer = windsorContainer;
        }

        public void Register<T>(T instance)
        {
            _windsorContainer.Register(
                Component.For(typeof(T)).Instance(instance)
            );
        }

        [DebuggerStepThrough]
        public T Resolve<T>()
        {
            return _windsorContainer.Resolve<T>();
        }

        [DebuggerStepThrough]
        public T Resolve<T>(string name)
        {
            Check.NotNullOrEmpty(name, "name");
            return _windsorContainer.Resolve<T>(name);
        }

        [DebuggerStepThrough]
        public IEnumerable<T> ResolveAll<T>()
        {
            return _windsorContainer.ResolveAll<T>();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing && _windsorContainer != null)
                _windsorContainer.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~WindsorContainerAdapter()
        {
            Dispose(false);
        }
    }
}