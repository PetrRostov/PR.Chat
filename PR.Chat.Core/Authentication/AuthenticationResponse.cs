using PR.Chat.Core.BusinessObjects;

namespace PR.Chat.Core.Authentication
{
    public class AuthenticationResponse
    {
        public AuthenticationResponse(bool success, IAccount account, string errorMessage)
        {
            Success = success;
            Account = account;
            ErrorMessage = errorMessage;
        }

        public AuthenticationResponse(string errorMessage) 
            : this (false, null, errorMessage)
        {
            
        }

        public AuthenticationResponse(IAccount account) :
            this(true, account, null)
        {
            
        }

        public bool Success { get; private set;}

        public string ErrorMessage { get; private set;}

        public IAccount Account { get; private set;}
    }
}