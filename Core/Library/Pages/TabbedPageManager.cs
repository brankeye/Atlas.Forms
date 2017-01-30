using System;
using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class TabbedPageManager : MultiPageManager<Page>, ITabbedPageManager
    {
        public TabbedPageManager(
            TabbedPage page,
            INavigationController navigationController,
            IPageCacheController pageCacheController) 
            : base(page, navigationController, pageCacheController)
        {

        }
    }
}
