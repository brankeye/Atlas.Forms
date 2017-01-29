using System;
using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Caching;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class PageFactory : IPageFactory
    {
        public PageFactory()
        {
            
        }

        public object GetNewPage(string key)
        {
            Type pageType;
            PageNavigationStore.Current.PageTypes.TryGetValue(key, out pageType);
            var nextPage = Activator.CreateInstance(pageType) as Page;
            return nextPage;
        }
    }
}
