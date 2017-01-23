using System.Collections.Generic;
using atlas.core.Library.Pages.Containers;

namespace atlas.core.Library.Caching
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
