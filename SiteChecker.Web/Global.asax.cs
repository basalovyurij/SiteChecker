using Ninject.Web.Common.WebHost;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using System;
using SiteChecker.Core;
using System.IO;
using Microsoft.Owin.Security;
using System.Web;
using SiteChecker.Logic.Interfaces;
using SiteChecker.Logic.Implementation;

namespace SiteChecker.Web
{
    public class MvcApplication : NinjectHttpApplication
    {
        private static IKernel _kernel;
        
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override IKernel CreateKernel()
        {
            if (_kernel == null)
            {
                _kernel = new StandardKernel();
                Registration(_kernel);
            }
            return _kernel;
        }

        private void Registration(IKernel kernel)
        {
            kernel.Bind<Config>().ToMethod(c => ConfigManager.Get()).InSingletonScope();
            kernel.Bind<IAuthenticationManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Authentication);
            kernel.Bind<ISiteCheckProvider>().To<SiteCheckProvider>();
            kernel.Bind<ISiteCheckManager>().To<SiteCheckManager>().InSingletonScope();
            kernel.Bind<ISiteEditManager>().To<SiteEditManager>();
        }
    }
}
