using System.Threading.Tasks;

namespace Kernel.Authentication.Services
{
    public interface IIndentityService
    {
        Task<AuthenticationResult> AuthenticateUser(IdentityAuthenicationContext context);
    }
}