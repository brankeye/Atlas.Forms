using atlas.core.Library.Pages;
using atlas.core.Library.Pages.Containers;

namespace atlas.core.Library.Interfaces
{
    public interface IPageCacheRegistry
    {
        FluentPageCacheContainer WhenAppears<TPage>();

        FluentPageCacheContainer WhenAppears(string pageKey);
    }
}
