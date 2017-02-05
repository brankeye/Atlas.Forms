using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Components
{
    public interface IServiceFactoryImp : IServiceFactory
    {
        INavigationService CreateNavigationService(INavigation navigation);

        INavigationController CreateNavigationController(INavigation navigation);

        IPageCacheController CreatePageCacheController();
    }
}
