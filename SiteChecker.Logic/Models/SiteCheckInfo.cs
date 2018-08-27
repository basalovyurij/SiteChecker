using System.Net;

namespace SiteChecker.Logic.Models
{
    public class SiteCheckInfo
    {
        public string Url { get; set; }
        public bool? IsAvaliable { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public int? ResponseTime { get; set; }
    }
}
