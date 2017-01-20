using System.Threading.Tasks;
using atlas.core.Library.Pages;
using atlas.core.Library.Pages.Containers;
using Xamarin.Forms;

namespace atlas.core.Library.Interfaces
{
    public interface IPageCacheCoordinator
    {
        Page GetCachedOrNewPage(string key, IParametersService parameters = null);

        Task RemoveCachedPages(string key);

        Task LoadCachedPages(string key);

        void AddPageToCache(string key);

        void AddPageToCache(string key, PageMapContainer container);
    }
}
