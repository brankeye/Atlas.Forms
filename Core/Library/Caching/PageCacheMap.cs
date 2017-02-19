using System.Collections.Generic;
using System.Collections.ObjectModel;
using Atlas.Forms.Infos;
using Atlas.Forms.Interfaces;

namespace Atlas.Forms.Caching
{
    public class PageCacheMap : IPageCacheMap
    {
        protected IDictionary<string, IList<MapInfo>> Mappings { get; } = new Dictionary<string, IList<MapInfo>>();

        public void AddMapInfos(string key, IList<MapInfo> mapInfos)
        {
            Mappings.Add(key, mapInfos);
        }

        public IList<MapInfo> GetMapInfos(string key)
        {
            IList<MapInfo> mapInfos;
            Mappings.TryGetValue(key, out mapInfos);
            return mapInfos;
        }

        public IReadOnlyDictionary<string, IList<MapInfo>> GetMappings()
        {
            return new ReadOnlyDictionary<string, IList<MapInfo>>(Mappings);
        }
    }
}
