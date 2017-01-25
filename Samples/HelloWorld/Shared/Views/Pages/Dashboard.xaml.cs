using System;
using System.Collections.Generic;
using atlas.samples.helloworld.Shared.ViewModels;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class Dashboard
    {
        public Dashboard()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Dashboard();

            Detail = PageCacheService.Current.GetCachedOrNewPage("About");

            var masterPageItems = new List<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = "Tutorials",
                    PageKey = "NavigationPage/Tutorials"
                },
                new MasterPageItem
                {
                    Title = "About",
                    PageKey = "About"
                },
                new MasterPageItem
                {
                    Title = "Changelog",
                    PageKey = "Changelog"
                },
                new MasterPageItem
                {
                    Title = "Contact",
                    PageKey = "Contact"
                }
            };

            listView.ItemsSource = masterPageItems;
            //var enumerator = listView.ItemsSource.GetEnumerator();
            //if (enumerator.MoveNext()) listView.SelectedItem = enumerator.Current;

            listView.ItemSelected += ListView_OnItemSelected;
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var selectedItem = (MasterPageItem) selectedItemChangedEventArgs.SelectedItem;
            (BindingContext as ViewModels.Pages.Dashboard)?.ChangePage(selectedItem.PageKey);
            IsPresented = false;
        }
    }
}
