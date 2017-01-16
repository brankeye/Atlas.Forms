using System.Collections.Generic;
using atlas.core.Library.Caching;
using Xamarin.Forms;

namespace atlas.core.Library.Interfaces
{
    public interface IPageCacheService
    {
        IReadOnlyDictionary<string, IList<PageCacheContainer>> PageCacheStore { get; }

        IReadOnlyDictionary<string, Page> CachedPages { get; }

        Page GetPage(string page);
    }
}
