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
        protected ICacheController CacheController { get; }

        protected IPageFactory PageFactory { get; }

        protected ICacheSubscriber CacheSubscriber { get; }

        protected IPageCacheMap PageCacheMap { get; }

        protected IPageKeyStore PageKeyStore { get; }

        public AutoCacheController(ICacheController cacheController, IPageCacheMap pageCacheMap, IPageKeyStore pageKeyStore, IPageFactory pageFactory, ICacheSubscriber cacheSubscriber)
        {
            CacheController = cacheController;
            PageCacheMap = pageCacheMap;
            PageFactory = pageFactory;
            PageKeyStore = pageKeyStore;
            CacheSubscriber = cacheSubscriber;
            SubscribeInternal();
        }

        private void SubscribeInternal()
        {
            Subscribe();
        }

        public virtual void Subscribe()
        {
            CacheSubscriber.SubscribePageAppeared(OnPageAppeared);
            CacheSubscriber.SubscribePageDisappeared(OnPageDisappeared);
            CacheSubscriber.SubscribePageNavigatedFrom(OnPageNavigatedFrom);
            CacheSubscriber.SubscribePageCreated(OnPageCreated);
        }

        public virtual void OnPageNavigatedFrom(Page page)
        {
            if (page == null) { return; }
            if (page is NavigationPage)
            {
                page = (page as NavigationPage).CurrentPage;
            }
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

        public virtual void OnPageAppeared(Page page)
        {
            if (page == null) { return; }
            if (page is NavigationPage)
            {
                page = (page as NavigationPage).CurrentPage;
            }
            var pageInfo = GetPageInfo(page);
            var pageMappings = GetPageMappings(pageInfo.Key);
            if (pageMappings != null)
            {
                pageMappings = pageMappings.Where(x => x.CacheOption == CacheOption.Appears).ToList();
                var currentPageMap = pageMappings.FirstOrDefault(x => x.Key == pageInfo.Key);
                if (currentPageMap != null)
                {
                    CacheController.TryAddCacheInfo(pageInfo.Key, new CacheInfo(page, true, currentPageMap));
                    pageMappings.Remove(currentPageMap);
                }
                AddPagesToCache(pageMappings);
            }
        }

        public virtual void OnPageDisappeared(Page page)
        {
            if (page == null) { return; }
            if (page is NavigationPage)
            {
                page = (page as NavigationPage).CurrentPage;
            }
            var pageInfo = GetPageInfo(page);
            var lifetimeMappings = GetLifetimeMappings(pageInfo.Key);
            foreach (var mapInfo in lifetimeMappings)
            {
                CacheController.RemoveCacheInfo(mapInfo.Key);
            }
        }

        public virtual void OnPageCreated(Page page)
        {
            if (page == null) { return; }
            if (page is NavigationPage)
            {
                page = (page as NavigationPage).CurrentPage;
            }
            var pageInfo = GetPageInfo(page);
            var pageMappings = GetPageMappings(pageInfo.Key);
            if (pageMappings != null)
            {
                pageMappings = pageMappings.Where(x => x.CacheOption == CacheOption.IsCreated).ToList();
                var currentPageMap = pageMappings.FirstOrDefault(x => x.Key == pageInfo.Key);
                if (currentPageMap != null)
                {
                    CacheController.TryAddCacheInfo(pageInfo.Key, new CacheInfo(page, true, currentPageMap));
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
                    CacheController.TryAddCacheInfo(mapInfo.Key, new CacheInfo(pageInstance, false, mapInfo));
                }
            }
        }

        protected virtual IPageInfo GetPageInfo(Page page)
        {
            var pageInfo = PageKeyStore.GetPageContainer(page);
            return pageInfo;
        }

        protected virtual IList<MapInfo> GetPageMappings(string key)
        {
            return PageCacheMap.GetMapInfos(key);
        }

        protected virtual IList<MapInfo> GetLifetimeMappings(string key)
        {
            var pageMappings = PageCacheMap.GetMappings().Values.Where(x => x.FirstOrDefault(y => y.LifetimePageKey == key) != null);
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

        public virtual void Unsubscribe()
        {
            CacheSubscriber.UnsubscribePageAppeared();
            CacheSubscriber.UnsubscribePageDisappeared();
            CacheSubscriber.UnsubscribePageNavigatedFrom();
            CacheSubscriber.UnsubscribePageCreated();
        }
    }
}
