using System;
using Atlas.Forms.Interfaces;
using Xamarin.Forms;

namespace Atlas.Forms.Navigation
{
    public class PageNavigationRegistry : IPageNavigationRegistry
    {
        public void RegisterPage<TPage>() where TPage : Page
        {
            RegisterPage<TPage>(typeof(TPage).Name);
        }

        public void RegisterPage<TPage, TClass>() where TPage : Page
        {
            RegisterPage<TPage>(typeof(TClass).Name);
        }

        public void RegisterPage<TPage>(string key) where TPage : Page
        {
            PageNavigationStore.Current.PageTypes[key] = typeof(TPage);
        }
    }
}
