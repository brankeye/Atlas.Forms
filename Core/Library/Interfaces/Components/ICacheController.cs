﻿using System.Collections.Generic;
using Atlas.Forms.Infos;

namespace Atlas.Forms.Interfaces.Components
{
    public interface ICacheController
    {
        IReadOnlyDictionary<string, CacheInfo> GetPageCache();

        CacheInfo TryGetCacheInfo(string key);

        bool TryAddCacheInfo(string key, CacheInfo info);

        void AddCacheInfos(IList<CacheInfo> list);

        bool RemoveCacheInfo(string key);
    }
}
