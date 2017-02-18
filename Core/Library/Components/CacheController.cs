using System.Collections.Generic;
using System.Collections.ObjectModel;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Pages.Infos;

namespace Atlas.Forms.Components
{
    public class CacheController : ICacheController
    {
        public IDictionary<string, CacheInfo> PageCache { get; } = new Dictionary<string, CacheInfo>();

        protected IPublisher Publisher { get; }

        public CacheController(IPublisher publisher)
        {
            Publisher = publisher;
        }

        public virtual IReadOnlyDictionary<string, CacheInfo> GetPageCache()
        {
            return new ReadOnlyDictionary<string, CacheInfo>(PageCache);
        }

        public virtual void AddCacheInfos(IList<CacheInfo> list)
        {
            foreach (var container in list)
            {
                CacheInfo info;
                PageCache.TryGetValue(container.TargetPageInfo.Key, out info);
                if (info == null)
                {
                    TryAddCacheInfo(container.TargetPageInfo.Key, container);
                }
            }
        }

        public virtual bool TryAddCacheInfo(string key, CacheInfo info)
        {
            CacheInfo storedInfo;
            PageCache.TryGetValue(key, out storedInfo);
            if (storedInfo == null)
            {
                Publisher.SendPageCachingMessage(info.Page);
                PageCache.Add(key, info);
                Publisher.SendPageCachedMessage(info.Page);
                return true;
            }
            return false;
        }

        public virtual CacheInfo TryGetCacheInfo(string key)
        {
            CacheInfo info;
            PageCache.TryGetValue(key, out info);
            if (info?.TargetPageInfo.CacheState == CacheState.Default)
            {
                RemoveCacheInfo(key);
            }
            return info;
        }

        public virtual bool RemoveCacheInfo(string key)
        {
            return PageCache.Remove(key);
        }
    }
}
