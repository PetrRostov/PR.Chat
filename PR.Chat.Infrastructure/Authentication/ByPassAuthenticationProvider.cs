using System;
using System.Diagnostics.Contracts;

namespace PR.Chat.Infrastructure.Authentication
{
    public class ByPassAuthenticationProvider : IAuthenticationProvider
    {
        private readonly IAccountService _accountService;
        
        public ByPassAuthenticationProvider(IAccountService accountService)
        {
            Contract.Requires<ArgumentException>(accountService != null);
            
            _accountService = accountService;
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest authenticationRequest)
        {
            var fields = authenticationRequest.Properties;

            if (!fields.ContainsKey(AuthenticationFields.Login))
                throw new ArgumentException(AuthenticationFields.Login);

            if (!fields.ContainsKey(AuthenticationFields.Password))
                throw new ArgumentException(AuthenticationFields.Password);

            var login       = (string) fields[AuthenticationFields.Login];
            var password    = (string) fields[AuthenticationFields.Password];

            try
            {
                var account = _accountService.Auth(login, password).Account;

                return AuthenticationResponse.OK(account);
            }
            catch (LoginNotFoundException)
            {
                return AuthenticationResponse.Error("LoginNotFound");
            }
            catch (WrongPasswordException)
            {
                return AuthenticationResponse.Error("WrongPassword");
            }
        }
    }

    public interface IAccountService
    {
        AuthResult Auth(string login, string password);
    }

    public class AuthResult
    {
        private IAccount _account;

        public IAccount Account
        {
            get {
                return _account;
            }
        }
    }

    public interface IAccount
    {
    }

    [Serializable]
    public class WrongPasswordException : Exception
    {
    }

    [Serializable]
    public class LoginNotFoundException : Exception
    {
    }
}