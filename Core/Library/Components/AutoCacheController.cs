using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Caching;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages.Info;
using Atlas.Forms.Services;
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
            MessagingService.Current.Subscribe<Page>(CacheEvents.OnPageAppeared, OnPageAppeared);
            MessagingService.Current.Subscribe<Page>(CacheEvents.OnPageDisappeared, OnPageDisappeared);
            MessagingService.Current.Subscribe<Page>(CacheEvents.OnPageCreated, OnPageCreated);
        }

        protected virtual void OnPageAppeared(IMessagingService messagingService, Page page)
        {
            var pageMappings = GetPageMappings(page);
            if (pageMappings != null)
            {
                pageMappings = pageMappings.Where(x => x.CacheOption == CacheOption.Appears).ToList();
            }
        }

        protected virtual void OnPageDisappeared(IMessagingService messagingService, Page page)
        {
            var pageMappings = GetPageMappings(page);
            if (pageMappings != null)
            {
                CacheController.RemoveCachedPages("");
            }
        }

        protected virtual void OnPageCreated(IMessagingService messagingService, Page page)
        {
            var pageMappings = GetPageMappings(page);
            if (pageMappings != null)
            {
                pageMappings = pageMappings.Where(x => x.CacheOption == CacheOption.IsCreated).ToList();
            }
        }

        protected virtual IList<PageMapInfo> GetPageMappings(Page page)
        {
            var pageInfo = PageKeyStore.Current.GetPageContainer(page);
            IList<PageMapInfo> pageMappings;
            PageCacheMap.Current.Mappings.TryGetValue(pageInfo.Key, out pageMappings);
            return pageMappings;
        }

        protected virtual void Unsubscribe()
        {
            MessagingService.Current.Unsubscribe(CacheEvents.OnPageAppeared);
            MessagingService.Current.Unsubscribe(CacheEvents.OnPageDisappeared);
            MessagingService.Current.Unsubscribe(CacheEvents.OnPageCreated);
        }
    }
}
