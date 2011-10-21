namespace PR.Chat.Infrastructure.Authentication
{
    public interface IAuthenticationProvider
    {
        AuthenticationResponse Authenticate(AuthenticationRequest authenticationRequest);
    }
}