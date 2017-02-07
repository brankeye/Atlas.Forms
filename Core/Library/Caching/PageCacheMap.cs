using System.Collections.Generic;
using System.Collections.ObjectModel;
using Atlas.Forms.Pages.Infos;

namespace Atlas.Forms.Caching
{
    public class PageCacheMap
    {
        public static PageCacheMap Current { get; set; } = new PageCacheMap();

        public IDictionary<string, IList<MapInfo>> Mappings { get; } = new Dictionary<string, IList<MapInfo>>();

        public IReadOnlyDictionary<string, IList<MapInfo>> GetMappings()
        {
            return new ReadOnlyDictionary<string, IList<MapInfo>>(Mappings);
        }
    }
}
