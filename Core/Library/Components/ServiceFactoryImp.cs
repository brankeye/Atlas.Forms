using System;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class ServiceFactoryImp : ServiceFactory, IServiceFactoryImp
    {
        public static IServiceFactoryImp Current { get; set; } = new ServiceFactoryImp();

        public INavigationController CreateNavigationController(INavigation navigation)
        {
            return CreateService<INavigationController>(navigation);
        }

        public INavigationService CreateNavigationService(INavigation navigation)
        {
            return CreateService<INavigationService>(navigation);
        }

        public IPageCacheController CreatePageCacheController()
        {
            return CreateService<IPageCacheController>();
        }
    }
}
