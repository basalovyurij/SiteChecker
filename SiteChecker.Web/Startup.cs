using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SiteChecker.Web.Startup))]
namespace SiteChecker.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
