using System.Collections.Generic;
using Atlas.Forms.Pages.Containers;

namespace Atlas.Forms.Caching
{
    internal class PageCacheMap
    {
        public static Dictionary<string, IList<PageMapContainer>> Mappings { get; } = new Dictionary<string, IList<PageMapContainer>>();

        public static IReadOnlyDictionary<string, IList<PageMapContainer>> GetMappings()
        {
            return Mappings;
        }
    }
}
