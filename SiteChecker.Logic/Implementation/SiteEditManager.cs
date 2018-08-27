using SiteChecker.Core;
using SiteChecker.Logic.Interfaces;

namespace SiteChecker.Logic.Implementation
{
    public class SiteEditManager : ISiteEditManager
    {
        private readonly Config _config;

        public SiteEditManager(Config config)
        {
            _config = config;
        }

        public bool HasSite(string url)
        {
            return _config.Urls.Contains(url);
        }

        public void AddSite(string url)
        {
            _config.Urls.Add(url);
            ConfigManager.Save(_config);
        }

        public void DeleteSite(string url)
        {
            _config.Urls.Remove(url);
            ConfigManager.Save(_config);
        }
    }
}
