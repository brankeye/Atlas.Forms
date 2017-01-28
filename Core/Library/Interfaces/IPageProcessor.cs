using Xamarin.Forms;

namespace Atlas.Forms.Interfaces
{
    public interface IPageProcessor
    {
        void AddBehaviors(Page page);

        void AddManagers(Page page, IPageCacheCoordinator cacheCoordinator);
    }
}
