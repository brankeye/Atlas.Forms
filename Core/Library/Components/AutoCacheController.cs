using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Enums;
using Atlas.Forms.Infos;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class AutoCacheController : IAutoCacheController
    {
        protected ICacheController CacheController { get; }

        protected IPageFactory PageFactory { get; }

        protected IPageCacheMap PageCacheMap { get; }

        protected IPageKeyStore PageKeyStore { get; }

        public AutoCacheController(ICacheController cacheController, 
                                   IPageCacheMap pageCacheMap, 
                                   IPageKeyStore pageKeyStore,
                                   IPageFactory pageFactory)
        {
            CacheController = cacheController;
            PageCacheMap = pageCacheMap;
            PageFactory = pageFactory;
            PageKeyStore = pageKeyStore;
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
                pageMappings = pageMappings.Where(x => x.TargetPageInfo.CacheState == CacheState.KeepAlive).ToList();
                foreach (var mapInfo in pageMappings)
                {
                    CacheController.RemoveCacheInfo(mapInfo.TargetPageInfo.Key);
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
                pageMappings = pageMappings.Where(x => x.TriggerPageInfo.TriggerOption == TriggerOption.Appears).ToList();
                var currentPageMap = pageMappings.FirstOrDefault(x => x.TargetPageInfo.Key == pageInfo.Key);
                if (currentPageMap != null)
                {
                    CacheController.TryAddCacheInfo(pageInfo.Key, new CacheInfo(page, true, currentPageMap.TargetPageInfo));
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
                CacheController.RemoveCacheInfo(mapInfo.TargetPageInfo.Key);
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
                pageMappings = pageMappings.Where(x => x.TriggerPageInfo.TriggerOption == TriggerOption.IsCreated).ToList();
                var currentPageMap = pageMappings.FirstOrDefault(x => x.TargetPageInfo.Key == pageInfo.Key);
                if (currentPageMap != null)
                {
                    CacheController.TryAddCacheInfo(pageInfo.Key, new CacheInfo(page, true, currentPageMap.TargetPageInfo));
                    pageMappings.Remove(currentPageMap);
                }
                AddPagesToCache(pageMappings);
            }
        }

        protected virtual void AddPagesToCache(IList<MapInfo> mapInfos)
        {
            foreach (var mapInfo in mapInfos)
            {
                var cacheInfo = CacheController.TryGetCacheInfo(mapInfo.TargetPageInfo.Key);
                if (cacheInfo == null)
                {
                    var pageInstance = PageFactory.GetNewPage(mapInfo.TargetPageInfo.Key);
                    CacheController.TryAddCacheInfo(mapInfo.TargetPageInfo.Key, new CacheInfo(pageInstance, false, mapInfo.TargetPageInfo));
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
            var pageMappings = PageCacheMap.GetMappings().Values.Where(x => x.FirstOrDefault(y => y.TargetPageInfo.LifetimeInstanceKey == key) != null);
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
    }
}
