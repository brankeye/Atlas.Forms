using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Pages.Infos;
using Xamarin.Forms;

namespace Atlas.Forms.Navigation
{
    public class PageKeyStore : IPageKeyStore
    {
        protected ConditionalWeakTable<Page, string> PageKeys { get; } = new ConditionalWeakTable<Page, string>();

        public void AddPageKey(Page page, string key)
        {
            PageKeys.Add(page, key);
        }

        public IPageInfo GetPageContainer(Page pageInstance)
        {
            if (pageInstance == null) return null;
            string pageKey;
            PageKeys.TryGetValue(pageInstance, out pageKey);
            return new PageInfo(pageKey, pageInstance.GetType());
        }

        public IList<IPageInfo> GetPageContainers(IList<Page> pages)
        {
            var pageContainers = new List<IPageInfo>();
            foreach (var page in pages)
            {
                pageContainers.Add(GetPageContainer(page));
            }
            return pageContainers;
        }
    }
}
