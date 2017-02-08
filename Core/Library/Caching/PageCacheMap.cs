using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Atlas.Forms.Pages.Infos;

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

    public interface IPageCacheMap
    {
        IReadOnlyDictionary<string, IList<MapInfo>> GetMappings();

        IList<MapInfo> GetMapInfos(string key);

        void AddMapInfos(string key, IList<MapInfo> mapInfos);
    }
}
