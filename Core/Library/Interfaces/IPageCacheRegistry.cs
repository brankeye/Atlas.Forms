namespace atlas.core.Library.Interfaces
{
    public interface IPageCacheRegistry
    {
        void RegisterPageForCache<TPage, TCachedPage>();

        void RegisterPageForCache<TPage>(string cachedPageKey);

        void RegisterPageForCache(string pageKey, string cachedPageKey);
    }
}
