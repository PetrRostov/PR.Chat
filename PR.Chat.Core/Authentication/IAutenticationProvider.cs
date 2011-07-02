using System.Collections.Specialized;

namespace PR.Chat.Core.Authentication
{
    public interface IAutenticationProvider
    {
        AuthenticationResponse Authenticate(AuthenticationRequest authenticationRequest);
    }
}