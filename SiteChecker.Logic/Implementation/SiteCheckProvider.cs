using SiteChecker.Logic.Interfaces;
using SiteChecker.Logic.Models;
using System.Net;

namespace SiteChecker.Logic.Implementation
{
    public class SiteCheckProvider : ISiteCheckProvider
    {
        public SiteCheckInfo GetInfo(HttpStatusCode? statusCode, int? responseTime)
        {
            var res = new SiteCheckInfo
            {
                StatusCode = statusCode,
                ResponseTime = responseTime,
                IsAvaliable = false
            };

            if(statusCode.HasValue && HttpStatusCode.OK == statusCode.Value)
            {
                res.IsAvaliable = true;
            }

            return res;
        }
    }
}
