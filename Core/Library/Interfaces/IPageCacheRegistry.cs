using atlas.core.Library.Pages;

namespace atlas.core.Library.Interfaces
{
    public interface IPageCacheRegistry
    {
        FluentPageCacheContainer WhenAppears<TPage>();

        FluentPageCacheContainer WhenAppears(string pageKey);
    }
}
