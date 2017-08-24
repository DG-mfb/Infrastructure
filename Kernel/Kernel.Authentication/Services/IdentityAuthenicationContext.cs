using System.Collections.Generic;
using System.Security;
using Kernel.Extensions;

namespace Kernel.Authentication.Services
{
    public class IdentityAuthenicationContext
    {
        public IdentityAuthenicationContext(string userName, string userEmail, ref string password, IEnumerable<string> authenticationTypes)
        {
            this.UserName = userName;
            this.UserEmail = userEmail;
            this.Password = StringExtensions.ToSecureString(password);
            password = null;
            this.AuthenticationTypes = authenticationTypes;
            
        }
        public string UserName { get; private set; }
        public string UserEmail { get; private set; }
        public SecureString Password { get; private set; }
        public IEnumerable<string> AuthenticationTypes { get; private set; }
    }
}