﻿using System;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Utilities;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class ServiceFactoryImp : ServiceFactory, IServiceFactoryImp
    {
        public void AddNavigationService(Func<INavigation, object> func)
        {
            AddService(typeof(INavigationService), args => func.Invoke(args[0] as INavigation));
        }

        public void AddNavigationController(Func<INavigation, object> func)
        {
            AddService(typeof(INavigationController), args => func.Invoke(args[0] as INavigation));
        }

        public void AddPageCacheController(Func<object> func)
        {
            AddService(typeof(IPageCacheController), args => func.Invoke());
        }

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
