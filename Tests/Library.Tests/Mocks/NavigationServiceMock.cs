﻿using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Services;

namespace Library.Tests.Mocks
{
    public class NavigationServiceMock : NavigationService
    {
        public NavigationServiceMock(INavigationController navigationController, IPageCacheController pageCacheController)
            : base(navigationController, pageCacheController) { }
    }
}
