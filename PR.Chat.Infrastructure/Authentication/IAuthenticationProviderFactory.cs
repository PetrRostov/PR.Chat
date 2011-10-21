using System;

namespace PR.Chat.Infrastructure.Authentication
{
    public interface IAuthenticationProviderFactory
    {
        IAuthenticationProvider Create(AuthenticationMethod method);
    }

    public class AuthenticationProviderFactoryByDependencyResolver : IAuthenticationProviderFactory
    {
        private readonly IDependencyResolver _dependencyResolver;

        private const string ByPassProviderKey = "ByPass";

        public AuthenticationProviderFactoryByDependencyResolver(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public IAuthenticationProvider Create(AuthenticationMethod method)
        {
            switch (method)
            {
                case AuthenticationMethod.ByPass :
                    return _dependencyResolver.Resolve<IAuthenticationProvider>(ByPassProviderKey);
                default:
                    throw new ArgumentException();
            }
        }
    }
}