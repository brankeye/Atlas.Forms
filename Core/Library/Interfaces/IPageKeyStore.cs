using System.Collections.Generic;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces
{
    public interface IPageKeyStore
    {
        IPageInfo GetPageContainer(Page pageInstance);

        IList<IPageInfo> GetPageContainers(IList<Page> pages);

        void AddPageKey(Page page, string key);
    }
}
