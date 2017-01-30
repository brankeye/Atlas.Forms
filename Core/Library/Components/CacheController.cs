using System;
using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Caching;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class CacheController : ICacheController
    {
        public void AddCachedPages(IList<PageCacheContainer> list)
        {
            foreach (var container in list)
            {
                PageCacheContainer pageContainer;
                PageCacheStore.Current.PageCache.TryGetValue(container.Key, out pageContainer);
                if (pageContainer == null)
                {
                    AddPage(container.Key, container);
                }
            }
        }

        public void AddPage(string key, PageCacheContainer container)
        {
            PageCacheContainer storedContainer;
            PageCacheStore.Current.PageCache.TryGetValue(key, out storedContainer);
            if (storedContainer == null)
            {
                PageActionInvoker.InvokeOnPageCaching(container.Page);
                PageCacheStore.Current.PageCache.Add(key, container);
                PageActionInvoker.InvokeOnPageCached(container.Page);
            }
        }

        public void RemoveCachedPages(string key)
        {
            IList<PageMapContainer> containers;
            PageCacheMap.Current.Mappings.TryGetValue(key, out containers);
            if (containers == null)
            {
                return;
            }
            foreach (var container in containers)
            {
                if (container.CacheState == CacheState.Default ||
                    container.CacheState == CacheState.KeepAlive)
                {
                    PageCacheStore.Current.PageCache.Remove(container.Key);
                }
            }
        }

        public object TryGetCachedPage(string key, IParametersService parameters)
        {
            PageCacheContainer container;
            PageCacheStore.Current.PageCache.TryGetValue(key, out container);
            if (container != null)
            {
                if (container.CacheState == CacheState.Default)
                {
                    PageCacheStore.Current.PageCache.Remove(container.Key);
                }
                if (!container.Initialized)
                {
                    PageActionInvoker.InvokeInitialize(container.Page, parameters);
                    container.Initialized = true;
                }
                return container.Page;
            }
            return null;
        }
    }
}
