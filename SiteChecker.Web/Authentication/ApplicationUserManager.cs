using Microsoft.AspNet.Identity;
using SiteChecker.Web.Models;

namespace SiteChecker.Web.Authentication
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(ApplicationUserStore store)
            : base(store)
        {
        }
    }
}