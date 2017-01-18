using System.Collections.Generic;
using atlas.core.Library.Pages;
using Xamarin.Forms;

namespace atlas.core.Library.Interfaces
{
    public interface IPageCacheService
    {
        IReadOnlyDictionary<string, IList<PageMapContainer>> PageCacheStore { get; }

        IReadOnlyDictionary<string, PageCacheContainer> CachedPages { get; }

        Page GetPage(string page);
    }
}
