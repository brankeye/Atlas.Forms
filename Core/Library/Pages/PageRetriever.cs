using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atlas.core.Library.Behaviors;
using atlas.core.Library.Caching;
using atlas.core.Library.Navigation;
using Xamarin.Forms;

namespace atlas.core.Library.Pages
{
    public class PageRetriever
    {
        public static Page GetPage(string key)
        {
            Page nextPage;
            if (PageKeyParser.IsSequence(key))
            {
                var queue = PageKeyParser.GetPageKeysFromSequence(key);
                var outerPageKey = queue.Dequeue();
                var innerPageKey = queue.Dequeue();
                var outerPageType = PageNavigationStore.GetPageType(outerPageKey);
                var innerPageType = PageNavigationStore.GetPageType(innerPageKey);
                var innerPage = PageCacheStore.TryGetPage(innerPageKey) ??
                                Activator.CreateInstance(innerPageType) as Page;
                nextPage = Activator.CreateInstance(outerPageType, innerPage) as Page;
                (nextPage as NavigationPage)?.Behaviors.Add(new NavigationPageBackButtonBehavior());
                PageCache.PreloadCachedPages(innerPageKey);
            }
            else
            {
                var pageType = PageNavigationStore.GetPageType(key);
                nextPage = PageCacheStore.TryGetPage(key) ??
                                Activator.CreateInstance(pageType) as Page;
                PageCache.PreloadCachedPages(key);
            }
            return nextPage;
        }
    }
}
