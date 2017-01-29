using System;
using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Caching;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class PageCacheController : IPageCacheController
    {
        protected IPageFactory PageFactory { get; set; }

        protected ICacheController CacheController { get; set; }

        public PageCacheController(IPageFactory pageFactory, ICacheController cacheController)
        {
            PageFactory = pageFactory;
            CacheController = cacheController;
        }

        public object GetCachedOrNewPage(string key, IParametersService parameters)
        {
            var page = CacheController.TryGetCachedPage(key, parameters) as Page;
            if (page != null)
            {
                return page;
            }
            return PageFactory.GetNewPage(key);
        }

        public void AddCachedPages(string key)
        {
            AddCachedPagesWithOption(key, CacheOption.None);
        }

        public void AddCachedPagesWithOption(string key, CacheOption cacheOption)
        {
            if (PageKeyParser.IsSequence(key))
            {
                var queue = PageKeyParser.GetPageKeysFromSequence(key);
                var outerPageKey = queue.Dequeue();
                key = queue.Dequeue();
            }

            IList<PageMapContainer> containers;
            PageCacheMap.Current.Mappings.TryGetValue(key, out containers);
            if (containers == null)
            {
                return;
            }
            if (cacheOption != CacheOption.None)
            {
                containers = containers.ToList().Where(x => x.CacheOption == cacheOption).ToList();
            }
            IList<PageCacheContainer> pageCacheList = new List<PageCacheContainer>();
            foreach (var container in containers)
            {
                PageCacheContainer pageContainer;
                PageCacheStore.Current.PageCache.TryGetValue(container.Key, out pageContainer);
                if (pageContainer == null)
                {
                    var page = PageFactory.GetNewPage(container.Key) as Page;
                    pageCacheList.Add(new PageCacheContainer(page, new PageMapContainer(container)));
                }
            }
            CacheController.AddCachedPages(pageCacheList);
        }

        public void RemoveCachedPages(string key)
        {
            CacheController.RemoveCachedPages(key);
        }
    }
}
