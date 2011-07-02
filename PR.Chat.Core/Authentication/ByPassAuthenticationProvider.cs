using System;
using System.Diagnostics.Contracts;
using PR.Chat.Core.BusinessObjects;
using PR.Chat.Core.Resources;
using PR.Chat.Core.Services;

namespace PR.Chat.Core.Authentication
{
    public class ByPassAuthenticationProvider : IAutenticationProvider
    {
        private readonly IAccountService _accountService;
        private readonly IResourceManager _resourceManager;

        public ByPassAuthenticationProvider(IAccountService accountService, IResourceManager resourceManager)
        {
            Contract.Requires<ArgumentException>(accountService != null);
            Contract.Requires<ArgumentException>(resourceManager != null);

            _accountService = accountService;
            _resourceManager = resourceManager;
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

                return new AuthenticationResponse(account);
            }
            catch (LoginNotFoundException)
            {
                return new AuthenticationResponse(_resourceManager.LoginNotFound);
            }
            catch (WrongPasswordException)
            {
                return new AuthenticationResponse(_resourceManager.WrongPassword);
            }
        }
    }
}