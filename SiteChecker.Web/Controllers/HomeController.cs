using SiteChecker.Logic.Interfaces;
using SiteChecker.Web.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SiteChecker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISiteCheckManager _siteCheckManager;

        public HomeController(ISiteCheckManager siteCheckManager)
        {
            _siteCheckManager = siteCheckManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCheckInfo()
        {
            var info = _siteCheckManager.GetInfo();

            var models = info
                .Select(t => new CheckResultModel
                {
                    Url = t.Url,
                    ResponseTime = t.ResponseTime.HasValue ? $"{t.ResponseTime} ms" : "",
                    AvaliabilityClass = t.IsAvaliable.HasValue ? (t.IsAvaliable.Value ? "success" : "danger") : "secondary",
                    StatusCodeClass = t.StatusCode == HttpStatusCode.OK ? "ok" : "",
                    StatusCodeName = t.StatusCode.HasValue ? $"{(int)t.StatusCode} {t.StatusCode}" : ""
                })
                .ToArray();

            return PartialView("_CheckInfo", models);
        }
    }
}