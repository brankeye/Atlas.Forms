using Atlas.Forms.Caching;
using Atlas.Forms.Navigation;

namespace Library.Tests.Helpers
{
    public class StateManager
    {
        public static void ResetAll()
        {
            ResetPageCacheMap();
            ResetPageKeyStore();
        }

        public static void ResetPageCacheMap()
        {
            PageCacheMap.Current = new PageCacheMap();
        }

        public static void ResetPageKeyStore()
        {
            PageKeyStore.Current = new PageKeyStore();
        }
    }
}
