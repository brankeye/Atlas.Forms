﻿using System;
using Atlas.Forms.Interfaces.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Components
{
    public interface IServiceFactoryImp : IServiceFactory
    {
        INavigationService CreateNavigationService(INavigation navigation);

        INavigationController CreateNavigationController(INavigation navigation);

        IPageCacheController CreatePageCacheController();

        void AddNavigationService(Func<INavigation, object> func);

        void AddNavigationController(Func<INavigation, object> func);

        void AddPageCacheController(Func<object> func);
    }
}
