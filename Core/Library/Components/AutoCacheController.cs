using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Caching;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages.Infos;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class AutoCacheController : IAutoCacheController
    {
        protected ICacheController CacheController { get; set; }

        public AutoCacheController(ICacheController cacheController)
        {
            CacheController = cacheController;
            SubscribeInternal();
        }

        private void SubscribeInternal()
        {
            Subscribe();
        }

        protected virtual void Subscribe()
        {
            CachePubSubService.Subscriber.SubscribePageAppeared(OnPageAppeared);
            CachePubSubService.Subscriber.SubscribePageDisappeared(OnPageDisappeared);
            CachePubSubService.Subscriber.SubscribePageCreated(OnPageCreated);
        }

        protected virtual void OnPageAppeared(Page page)
        {
            var pageMappings = GetPageMappings(page);
            if (pageMappings != null)
            {
                pageMappings = pageMappings.Where(x => x.CacheOption == CacheOption.Appears).ToList();
            }
        }

        protected virtual void OnPageDisappeared(Page page)
        {
            var pageMappings = GetPageMappings(page);
            if (pageMappings != null)
            {
                //CacheController.RemoveCachedPages("");
            }
        }

        protected virtual void OnPageCreated(Page page)
        {
            var pageMappings = GetPageMappings(page);
            if (pageMappings != null)
            {
                pageMappings = pageMappings.Where(x => x.CacheOption == CacheOption.IsCreated).ToList();
            }
        }

        protected virtual IList<MapInfo> GetPageMappings(Page page)
        {
            var pageInfo = PageKeyStore.Current.GetPageContainer(page);
            IList<MapInfo> pageMappings;
            PageCacheMap.Current.Mappings.TryGetValue(pageInfo.Key, out pageMappings);
            return pageMappings;
        }

        protected virtual void Unsubscribe()
        {
            CachePubSubService.Subscriber.UnsubscribePageAppeared();
            CachePubSubService.Subscriber.UnsubscribePageDisappeared();
            CachePubSubService.Subscriber.UnsubscribePageCreated();
        }
    }
}
