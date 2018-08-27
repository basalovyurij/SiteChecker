using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SiteChecker.Web.Models;

namespace SiteChecker.Web.Authentication
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }
    }
}