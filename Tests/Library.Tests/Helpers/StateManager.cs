using Atlas.Forms.Caching;
using Atlas.Forms.Navigation;

namespace Library.Tests.Helpers
{
    public class StateManager
    {
        public static void ResetAll()
        {
            ResetPageNavigationStore();
            ResetPageCacheStore();
            ResetPageCacheMap();
        }

        public static void ResetPageNavigationStore()
        {
            PageNavigationStore.Current = new PageNavigationStore();
        }

        public static void ResetPageCacheStore()
        {
            PageCacheStore.Current = new PageCacheStore();
        }

        public static void ResetPageCacheMap()
        {
            PageCacheMap.Current = new PageCacheMap();
        }
    }
}
