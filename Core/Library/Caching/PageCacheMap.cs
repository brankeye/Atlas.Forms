using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Atlas.Forms.Pages.Containers;

namespace Atlas.Forms.Caching
{
    public class PageCacheMap
    {
        public static PageCacheMap Current { get; set; } = new PageCacheMap();

        public IDictionary<string, IList<PageMapContainer>> Mappings { get; } = new Dictionary<string, IList<PageMapContainer>>();

        public IReadOnlyDictionary<string, IList<PageMapContainer>> GetMappings()
        {
            return new ReadOnlyDictionary<string, IList<PageMapContainer>>(Mappings);
        }
    }
}
