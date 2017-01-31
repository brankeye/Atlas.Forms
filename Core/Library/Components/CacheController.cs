using System;
using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Caching;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;

namespace Atlas.Forms.Components
{
    public class CacheController : ICacheController
    {
        public virtual void AddCachedPages(IList<PageCacheContainer> list)
        {
            foreach (var container in list)
            {
                PageCacheContainer pageContainer;
                PageCacheStore.Current.PageCache.TryGetValue(container.Key, out pageContainer);
                if (pageContainer == null)
                {
                    TryAddPage(container.Key, container);
                }
            }
        }

        public virtual bool TryAddPage(string key, PageCacheContainer container)
        {
            PageCacheContainer storedContainer;
            PageCacheStore.Current.PageCache.TryGetValue(key, out storedContainer);
            if (storedContainer == null)
            {
                PageActionInvoker.InvokeOnPageCaching(container.Page);
                PageCacheStore.Current.PageCache.Add(key, container);
                PageActionInvoker.InvokeOnPageCached(container.Page);
                return true;
            }
            return false;
        }

        public virtual void RemoveCachedPages(string key)
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

        public virtual object TryGetCachedPage(string key, IParametersService parameters)
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

        public virtual bool RemovePageFromCache(string key)
        {
            return PageCacheStore.Current.PageCache.Remove(key);
        }
    }
}
