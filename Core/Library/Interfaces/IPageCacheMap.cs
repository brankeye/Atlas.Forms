using System.Collections.Generic;
using Atlas.Forms.Pages.Infos;

namespace Atlas.Forms.Interfaces
{
    public interface IPageCacheMap
    {
        IReadOnlyDictionary<string, IList<MapInfo>> GetMappings();

        IList<MapInfo> GetMapInfos(string key);

        void AddMapInfos(string key, IList<MapInfo> mapInfos);
    }
}
