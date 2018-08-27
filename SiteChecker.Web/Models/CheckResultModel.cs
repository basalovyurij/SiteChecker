namespace SiteChecker.Web.Models
{
    public class CheckResultModel
    {
        public string Url { get; set; }
        public string AvaliabilityClass { get; set; }
        public string StatusCodeName { get; set; }
        public string StatusCodeClass { get; set; }
        public string ResponseTime { get; set; }
    }
}