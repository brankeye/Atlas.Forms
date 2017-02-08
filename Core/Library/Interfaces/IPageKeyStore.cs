using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
