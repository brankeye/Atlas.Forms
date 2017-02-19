using System.Collections.Generic;
using Atlas.Forms.Infos;
using Atlas.Forms.Pages;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Services
{
    public interface IPageCacheService
    {
        IReadOnlyDictionary<string, CacheInfo> CachedPages { get; }

        Page GetCachedOrNewPage(NavigationInfo pageInfo, IParametersService parameters = null);

        Page GetNewPage(NavigationInfo pageInfo, IParametersService parameters = null);

        Page TryGetCachedPage(NavigationInfo pageInfo, IParametersService parameters = null);

        bool TryAddPage(TargetPageInfo targetPageInfo);

        bool TryAddExistingPage(TargetPageInfo targetPageInfo, Page page);

        bool RemovePage(NavigationInfo pageInfo);
    }
}
