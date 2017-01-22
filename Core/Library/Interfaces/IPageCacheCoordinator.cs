using System.Threading.Tasks;
using atlas.core.Library.Enums;
using atlas.core.Library.Pages;
using atlas.core.Library.Pages.Containers;
using Xamarin.Forms;

namespace atlas.core.Library.Interfaces
{
    public interface IPageCacheCoordinator
    {
        Page GetCachedOrNewPage(string key, IParametersService parameters = null);

        void RemoveCachedPages(string key);

        void LoadCachedPages(string key, CacheOption option = CacheOption.None);

        void AddPageToCache(string key, PageMapContainer container, bool isInitialized = false);
    }
}
