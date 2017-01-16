using System.Collections.Generic;
using Xamarin.Forms;

namespace atlas.core.Library.Interfaces
{
    public interface IPageCacheService
    {
        IReadOnlyDictionary<string, IList<IPageContainer>> PageCacheStore { get; }

        IReadOnlyDictionary<string, Page> CachedPages { get; }

        Page GetPage(string page);
    }
}
