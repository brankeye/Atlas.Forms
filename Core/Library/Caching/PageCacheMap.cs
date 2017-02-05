using System.Collections.Generic;
using System.Collections.ObjectModel;
using Atlas.Forms.Pages.Info;

namespace Atlas.Forms.Caching
{
    public class PageCacheMap
    {
        public static PageCacheMap Current { get; set; } = new PageCacheMap();

        public IDictionary<string, IList<PageMapInfo>> Mappings { get; } = new Dictionary<string, IList<PageMapInfo>>();

        public IReadOnlyDictionary<string, IList<PageMapInfo>> GetMappings()
        {
            return new ReadOnlyDictionary<string, IList<PageMapInfo>>(Mappings);
        }
    }
}
