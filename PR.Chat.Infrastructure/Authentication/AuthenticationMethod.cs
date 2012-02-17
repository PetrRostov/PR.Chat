namespace PR.Chat.Infrastructure.Authentication
{
    public class AuthenticationMethod : Enumeration.Enumeration
    {
        public static readonly AuthenticationMethod LoginAndPassword = new AuthenticationMethod("LOGIN_PASS");
        public static readonly AuthenticationMethod OpenID = new AuthenticationMethod("OPEN_ID");


        public AuthenticationMethod(string value) : base(value)
        {
        }

        public AuthenticationMethod(string value, string displayName) : base(value, displayName)
        {
        }
    }
}