using System.Collections.Generic;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Pages.Infos;

namespace Atlas.Forms.Interfaces.Components
{
    public interface ICacheController
    {
        CacheInfo TryGetCacheInfo(string key);

        bool TryAddCacheInfo(string key, CacheInfo info);

        void AddCacheInfos(IList<CacheInfo> list);

        bool RemoveCacheInfo(string key);
    }
}
