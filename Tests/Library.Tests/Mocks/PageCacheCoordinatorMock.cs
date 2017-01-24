using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Caching;
using Atlas.Forms.Navigation;

namespace Library.Tests.Mocks
{
    public class PageCacheCoordinatorMock : PageCacheCoordinator
    {
        protected override PageNavigationStore GetPageNavigationStore()
        {
            return new PageNavigationStore();
        }

        protected override PageCacheMap GetPageCacheMap()
        {
            return new PageCacheMap();
        }

        protected override PageCacheStore GetPageCacheStore()
        {
            return new PageCacheStore();
        }
    }
}
