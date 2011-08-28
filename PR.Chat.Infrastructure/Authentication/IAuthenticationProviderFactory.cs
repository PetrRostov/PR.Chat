using System;
using PR.Chat.Core.DependencyResolver;

namespace PR.Chat.Core.Authentication
{
    public interface IAuthenticationProviderFactory
    {
        IAutenticationProvider Create(AuthenticationMethod method);
    }

    class AuthenticationProviderFactoryByDependencyResolver : IAuthenticationProviderFactory
    {
        private readonly IDependencyResolver _dependencyResolver;

        private const string ByPassProviderKey = "ByPass";

        public AuthenticationProviderFactoryByDependencyResolver(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public IAutenticationProvider Create(AuthenticationMethod method)
        {
            switch (method)
            {
                case AuthenticationMethod.ByPass :
                    return _dependencyResolver.Resolve<IAutenticationProvider>(ByPassProviderKey);
                default:
                    throw new ArgumentException();
            }
        }
    }
}