﻿using atlas.core.Library.Caching;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Navigation;
using atlas.core.Library.Services;

namespace atlas.core.Library
{
    public abstract class AtlasApplication : AtlasApplicationBase
    {
        protected override INavigationService CreateNavigationService()
        {
            return new NavigationService(new ApplicationProvider(), new PageCacheCoordinator());
        }

        protected override IPageCacheService CreatePageCacheService()
        {
            return new PageCacheService();
        }

        protected override IPageNavigationRegistry CreatePageNavigationRegistry()
        {
            return new PageNavigationRegistry();
        }

        protected override IPageCacheRegistry CreatePageCacheRegistry()
        {
            return new PageCacheRegistry();
        }
    }
}
