using Atlas.Forms.Caching;
using Atlas.Forms.Navigation;

namespace Library.Tests.Helpers
{
    public class StateManager
    {
        public static void ResetAll()
        {
            ResetPageKeyStore();
        }

        public static void ResetPageKeyStore()
        {
            PageKeyStore.Current = new PageKeyStore();
        }
    }
}
