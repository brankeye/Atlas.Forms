using System.Collections.Generic;
using Atlas.Forms.Pages.Containers;

namespace Atlas.Forms.Caching
{
    public class PageCacheMap
    {
        public static PageCacheMap Current { get; set; } = new PageCacheMap();

        public Dictionary<string, IList<PageMapContainer>> Mappings { get; } = new Dictionary<string, IList<PageMapContainer>>();

        public IReadOnlyDictionary<string, IList<PageMapContainer>> GetMappings()
        {
            return Mappings;
        }
    }
}
