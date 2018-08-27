using Microsoft.AspNet.Identity.Owin;
using SiteChecker.Logic.Interfaces;
using SiteChecker.Web.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SiteChecker.Web.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly ISiteCheckManager _siteCheckManager;
        private readonly ISiteEditManager _siteEditManager;

        public ManageController(ISiteCheckManager siteCheckManager, ISiteEditManager siteEditManager)
        {
            _siteCheckManager = siteCheckManager;
            _siteEditManager = siteEditManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var data = _siteCheckManager.GetInfo().Select(t => t.Url).ToArray();
            return PartialView("_List", data);
        }

        public JsonResult Add(string url)
        {
            if(_siteEditManager.HasSite(url))
                return Json(new { result = false });

            _siteCheckManager.AddSite(url);
            _siteEditManager.AddSite(url);
            return Json(new { result = true });
        }

        public JsonResult Delete(string url)
        {
            _siteCheckManager.DeleteSite(url);
            _siteEditManager.DeleteSite(url);
            return Json(new { result = true });
        }
    }
}