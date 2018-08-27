namespace SiteChecker.Logic.Interfaces
{
    public interface ISiteEditManager
    {
        bool HasSite(string url);
        void AddSite(string url);
        void DeleteSite(string url);
    }
}
