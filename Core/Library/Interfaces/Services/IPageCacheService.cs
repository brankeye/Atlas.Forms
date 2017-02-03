using System.Collections.Generic;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Services
{
    public interface IPageCacheService
    {
        IReadOnlyDictionary<string, PageCacheContainer> CachedPages { get; }

        Page GetCachedOrNewPage(NavigationInfo pageInfo, IParametersService parameters = null);

        Page GetNewPage(NavigationInfo pageInfo, IParametersService parameters = null);

        Page TryGetCachedPage(NavigationInfo pageInfo, IParametersService parameters = null);

        bool TryAddPage(NavigationInfo pageInfo);

        bool TryAddPageAsKeepAlive(NavigationInfo pageInfo);

        bool TryAddPageAsSingleInstance(NavigationInfo pageInfo);
    }
}
