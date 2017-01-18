using atlas.core.Library.Pages;
using Xamarin.Forms;

namespace atlas.core.Library.Interfaces
{
    public interface IPageCacheCoordinator
    {
        Page GetCachedOrNewPage(string key);

        void RemoveCachedPages(string key);

        void LoadCachedPages(string key);

        void AddPageToCache(string key);

        void AddPageToCache(PageMapContainer container);
    }
}
