using AspNet.EntityFramework.IdentityProvider.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet.EntityFramework.IdentityProvider
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}