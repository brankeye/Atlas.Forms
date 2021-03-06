﻿using System.Collections.Generic;
using atlas.samples.helloworld.Shared.ViewModels;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class MainMasterDetailPage : IMasterDetailPageProvider
    {
        public MainMasterDetailPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.MainMasterDetailPage();

            var masterPageItems = new List<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = "MyContentPage",
                    PageInfo = Nav.Get("MyContentPage").AsNavigationPage().Info()
                },
                new MasterPageItem
                {
                    Title = "MyTabbedPage",
                    PageInfo = Nav.Get("MyTabbedPage").Info()
                }
            };

            ListView.ItemsSource = masterPageItems;
            //var enumerator = listView.ItemsSource.GetEnumerator();
            //if (enumerator.MoveNext()) listView.SelectedItem = enumerator.Current;

            ListView.ItemSelected += ListView_OnItemSelected;
        }

        public IMasterDetailPageManager PageManager { get; set; }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var selectedItem = (MasterPageItem)selectedItemChangedEventArgs.SelectedItem;
            (BindingContext as ViewModels.Pages.MainMasterDetailPage)?.PresentPage(selectedItem.PageInfo);
            IsPresented = false;
        }
    }
}
