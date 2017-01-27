using System;
using System.Collections.Generic;
using atlas.samples.helloworld.Shared.ViewModels;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class Dashboard : IMasterDetailPageManager
    {
        public Dashboard()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Dashboard();

            Detail = PageService.Current.GetCachedOrNewPage("About");

            var masterPageItems = new List<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = "Tutorials",
                    PageKey = "Tutorials"
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

        public IPresenter PageController { get; set; }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var selectedItem = (MasterPageItem) selectedItemChangedEventArgs.SelectedItem;
            Detail = PageService.Current.GetCachedOrNewPage(selectedItem.PageKey);
            IsPresented = false;
        }
    }
}
