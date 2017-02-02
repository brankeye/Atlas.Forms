using System.Collections.Generic;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Services
{
    public interface IPageCacheService
    {
        IReadOnlyDictionary<string, PageCacheContainer> CachedPages { get; }

        Page GetCachedOrNewPage(string page, IParametersService parameters = null);

        Page GetNewPage(string page, IParametersService parameters = null);

        Page TryGetCachedPage(string page, IParametersService parameters = null);

        bool TryAddPage(string key);

        bool TryAddPageAsKeepAlive(string key);

        bool TryAddPageAsSingleInstance(string key);

        bool RemovePage(string key);

        Page GetCachedOrNewPage<TClass>(IParametersService parameters = null);

        Page GetNewPage<TClass>(IParametersService parameters = null);

        Page TryGetCachedPage<TClass>(IParametersService parameters = null);

        bool TryAddPage(string key, Page page);

        bool TryAddPage<TClass>(Page page);

        bool TryAddPage<TClass>();

        bool TryAddPageAsKeepAlive<TClass>();

        bool TryAddPageAsSingleInstance<TClass>();

        bool TryAddPageAsLifetimeInstance<TClass>();

        bool RemovePage<TClass>();
    }
}
