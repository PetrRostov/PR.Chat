namespace PR.Chat.Infrastructure.Authentication
{
    public class AuthenticationResponse
    {
        private AuthenticationResponse(bool success, IAccount account, string errorMessage)
        {
            Success = success;
            Account = account;
            ErrorMessage = errorMessage;
        }

        private AuthenticationResponse(string errorMessage) 
            : this (false, null, errorMessage)
        {
            
        }

        private AuthenticationResponse(IAccount account) :
            this(true, account, null)
        {
            
        }

        public bool Success { get; private set;}

        public string ErrorMessage { get; private set;}

        public IAccount Account { get; private set;}

        public static AuthenticationResponse OK(IAccount account)
        {
            return new AuthenticationResponse(account);
        }

        public static AuthenticationResponse Error(string errorMessage)
        {
            return new AuthenticationResponse(false, null, errorMessage);
        }
    }
}