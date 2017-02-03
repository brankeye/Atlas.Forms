using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Navigation
{
    public class PageKeyStore
    {
        public static PageKeyStore Current { get; set; } = new PageKeyStore();

        public ConditionalWeakTable<Page, string> PageKeys { get; } = new ConditionalWeakTable<Page, string>();

        public IPageContainer GetPageContainer(Page pageInstance)
        {
            string pageKey;
            PageKeys.TryGetValue(pageInstance, out pageKey);
            return new PageContainer(pageKey, pageInstance.GetType());
        }

        public IList<IPageContainer> GetPageContainers(IList<Page> pages)
        {
            var pageContainers = new List<IPageContainer>();
            foreach (var page in pages)
            {
                pageContainers.Add(GetPageContainer(page));
            }
            return pageContainers;
        }
    }
}
