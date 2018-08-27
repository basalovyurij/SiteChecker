using SiteChecker.Logic.Models;

namespace SiteChecker.Logic.Interfaces
{
    public interface ISiteCheckManager
    {
        void AddSite(string url);
        void DeleteSite(string url);
        SiteCheckInfo[] GetInfo();
        void ReCheck();
    }
}
