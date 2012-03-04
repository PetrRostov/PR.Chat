namespace PR.Chat.Infrastructure.Authentication
{
    public interface IAuthenticationProviderFactory
    {
        IAuthenticationProvider Create(AuthenticationMethod method);
    }

    public class AuthenticationProviderFactoryByDependencyResolver : IAuthenticationProviderFactory
    {
        private const string ByPassProviderKey = "ByPass";
        private readonly IDependencyResolver _dependencyResolver;

        public AuthenticationProviderFactoryByDependencyResolver(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        #region IAuthenticationProviderFactory Members

        public IAuthenticationProvider Create(AuthenticationMethod method)
        {
            return null;
            //switch (method)
            //{
            //    case AuthenticationMethod.ByPass :
            //        return _dependencyResolver.Resolve<IAuthenticationProvider>(ByPassProviderKey);
            //    default:
            //        throw new ArgumentException();
            //}
        }

        #endregion
    }
}