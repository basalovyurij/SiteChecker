using Microsoft.AspNet.Identity;
using SiteChecker.Core;

namespace SiteChecker.Web.Models
{
    public class ApplicationUser : IUser<string>
    {
        public ApplicationUser(ConfigUser user)
        {
            Id = user.Login;
            UserName = user.Login;
            Password = user.Password;
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}