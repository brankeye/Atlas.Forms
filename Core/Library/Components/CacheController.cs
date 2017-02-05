using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Caching;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Info;

namespace Atlas.Forms.Components
{
    public class CacheController : ICacheController
    {
        public virtual void AddCachedPages(IList<PageCacheInfo> list)
        {
            foreach (var container in list)
            {
                PageCacheInfo pageInfo;
                PageCacheStore.Current.PageCache.TryGetValue(container.Key, out pageInfo);
                if (pageInfo == null)
                {
                    TryAddPage(container.Key, container);
                }
            }
        }

        public virtual bool TryAddPage(string key, PageCacheInfo info)
        {
            PageCacheInfo storedInfo;
            PageCacheStore.Current.PageCache.TryGetValue(key, out storedInfo);
            if (storedInfo == null)
            {
                PageActionInvoker.InvokeOnPageCaching(info.Page);
                PageCacheStore.Current.PageCache.Add(key, info);
                PageActionInvoker.InvokeOnPageCached(info.Page);
                return true;
            }
            return false;
        }

        public virtual void RemoveCachedPages(string key)
        {
            IList<PageMapInfo> containers;
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

            var lists = PageCacheMap.Current.Mappings.Values;
            foreach (var list in lists)
            {
                var sorted = list.Where(x => x.CacheState == CacheState.LifetimeInstance && x.LifetimePageKey == key).ToList();
                foreach (var map in sorted)
                {
                    PageCacheStore.Current.PageCache.Remove(map.Key);
                }
            }
        }

        public virtual object TryGetCachedPage(string key, IParametersService parameters)
        {
            PageCacheInfo info;
            PageCacheStore.Current.PageCache.TryGetValue(key, out info);
            if (info != null)
            {
                if (info.CacheState == CacheState.Default)
                {
                    PageCacheStore.Current.PageCache.Remove(info.Key);
                }
                if (!info.Initialized)
                {
                    PageActionInvoker.InvokeInitialize(info.Page, parameters);
                    info.Initialized = true;
                }
                return info.Page;
            }
            return null;
        }

        public virtual bool RemovePageFromCache(string key)
        {
            return PageCacheStore.Current.PageCache.Remove(key);
        }
    }
}
