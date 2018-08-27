using System.Collections.Generic;

namespace SiteChecker.Core
{
    public class Config
    {
        public List<ConfigUser> Users { get; set; }
        public int CheckIntervalInMs { get; set; }
        public int CheckTimeoutInMs { get; set; }
        public List<string> Urls { get; set; }
    }
}
