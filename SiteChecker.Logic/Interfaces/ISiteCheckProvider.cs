using SiteChecker.Logic.Models;
using System.Net;

namespace SiteChecker.Logic.Interfaces
{
    public interface ISiteCheckProvider
    {
        SiteCheckInfo GetInfo(HttpStatusCode? statusCode, int? responseTime);
    }
}
