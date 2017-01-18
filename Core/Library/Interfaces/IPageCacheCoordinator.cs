using atlas.core.Library.Pages;
using atlas.core.Library.Pages.Containers;
using Xamarin.Forms;

namespace atlas.core.Library.Interfaces
{
    public interface IPageCacheCoordinator
    {
        Page GetCachedOrNewPage(string key);

        Page GetCachedPage(string key);

        void RemoveCachedPages(string key);

        void LoadCachedPages(string key);

        void AddPageToCache(string key);

        void AddPageToCache(string key, PageMapContainer container);
    }
}
