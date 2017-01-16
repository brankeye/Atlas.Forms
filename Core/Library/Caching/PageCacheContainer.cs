using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atlas.core.Library.Caching
{
    public class PageCacheContainer
    {
        public PageCacheContainer(string pageKey)
        {
            PageKey = pageKey;
        }

        public string PageKey { get; set; }
    }
}
