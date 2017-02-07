using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Pages.Infos;
using Xamarin.Forms;

namespace Atlas.Forms.Navigation
{
    public class PageKeyStore
    {
        public static PageKeyStore Current { get; set; } = new PageKeyStore();

        public ConditionalWeakTable<Page, string> PageKeys { get; } = new ConditionalWeakTable<Page, string>();

        public IPageInfo GetPageContainer(Page pageInstance)
        {
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
