namespace Kernel.Authentication.Services
{
    public class AuthenticationResult
    {
        public AuthenticationResult(AuthenticationResults authenticationResult)
        {
            this.Result = authenticationResult;
        }
        public AuthenticationResults Result { get; }
    }
}