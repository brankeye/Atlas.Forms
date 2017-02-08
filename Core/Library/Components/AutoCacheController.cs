using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Caching;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages.Infos;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class AutoCacheController : IAutoCacheController
    {
        protected ICacheController CacheController { get; set; }

        protected IPageFactory PageFactory { get; set; }

        public AutoCacheController(ICacheController cacheController, IPageFactory pageFactory)
        {
            CacheController = cacheController;
            PageFactory = pageFactory;
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
            CachePubSubService.Subscriber.SubscribePageNavigatedFrom(OnPageNavigatedFrom);
            CachePubSubService.Subscriber.SubscribePageCreated(OnPageCreated);
        }

        protected virtual void OnPageNavigatedFrom(Page page)
        {
            var pageInfo = GetPageInfo(page);
            var pageMappings = GetPageMappings(pageInfo.Key);
            if (pageMappings != null)
            {
                pageMappings = pageMappings.Where(x => x.CacheState == CacheState.KeepAlive).ToList();
                foreach (var mapInfo in pageMappings)
                {
                    CacheController.RemoveCacheInfo(mapInfo.Key);
                }
            }
        }

        protected virtual void OnPageAppeared(Page page)
        {
            var pageInfo = GetPageInfo(page);
            var pageMappings = GetPageMappings(pageInfo.Key);
            if (pageMappings != null)
            {
                pageMappings = pageMappings.Where(x => x.CacheOption == CacheOption.Appears).ToList();
                var currentPageMap = pageMappings.FirstOrDefault(x => x.Key == pageInfo.Key && x.CacheOption == CacheOption.Appears);
                if (currentPageMap != null)
                {
                    CacheController.TryAddCacheInfo(pageInfo.Key, new CacheInfo(page, currentPageMap));
                    pageMappings.Remove(currentPageMap);
                }
                AddPagesToCache(pageMappings);
            }
        }

        protected virtual void OnPageDisappeared(Page page)
        {
            var pageInfo = GetPageInfo(page);
            var lifetimeMappings = GetLifetimeMappings(pageInfo.Key);
            foreach (var mapInfo in lifetimeMappings)
            {
                CacheController.RemoveCacheInfo(mapInfo.Key);
            }
        }

        protected virtual void OnPageCreated(Page page)
        {
            var pageInfo = GetPageInfo(page);
            var pageMappings = GetPageMappings(pageInfo.Key);
            if (pageMappings != null)
            {
                pageMappings = pageMappings.Where(x => x.CacheOption == CacheOption.IsCreated).ToList();
                var currentPageMap = pageMappings.FirstOrDefault(x => x.Key == pageInfo.Key && x.CacheOption == CacheOption.IsCreated);
                if (currentPageMap != null)
                {
                    CacheController.TryAddCacheInfo(pageInfo.Key, new CacheInfo(page, currentPageMap));
                    pageMappings.Remove(currentPageMap);
                }
                AddPagesToCache(pageMappings);
            }
        }

        protected virtual void AddPagesToCache(IList<MapInfo> mapInfos)
        {
            foreach (var mapInfo in mapInfos)
            {
                var cacheInfo = CacheController.TryGetCacheInfo(mapInfo.Key);
                if (cacheInfo == null)
                {
                    var pageInstance = PageFactory.GetNewPage(mapInfo.Key);
                    CacheController.TryAddCacheInfo(mapInfo.Key, new CacheInfo(pageInstance, mapInfo));
                }
            }
        }

        protected virtual IPageInfo GetPageInfo(Page page)
        {
            var pageInfo = PageKeyStore.Current.GetPageContainer(page);
            return pageInfo;
        }

        protected virtual IList<MapInfo> GetPageMappings(string key)
        {
            IList<MapInfo> pageMappings;
            PageCacheMap.Current.Mappings.TryGetValue(key, out pageMappings);
            return pageMappings;
        }

        protected virtual IList<MapInfo> GetLifetimeMappings(string key)
        {
            var pageMappings = PageCacheMap.Current.Mappings.Values.Where(x => x.FirstOrDefault(y => y.LifetimePageKey == key) != null);
            IList<MapInfo> mapInfos = new List<MapInfo>();
            foreach (var list in pageMappings)
            {
                foreach (var item in list)
                {
                    mapInfos.Add(item);
                }
            }
            return mapInfos;
        }

        protected virtual void Unsubscribe()
        {
            CachePubSubService.Subscriber.UnsubscribePageAppeared();
            CachePubSubService.Subscriber.UnsubscribePageDisappeared();
            CachePubSubService.Subscriber.UnsubscribePageNavigatedFrom();
            CachePubSubService.Subscriber.UnsubscribePageCreated();
        }
    }
}
