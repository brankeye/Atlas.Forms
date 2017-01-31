using System;
using System.Collections.Generic;
using atlas.samples.helloworld.Shared.ViewModels;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class Dashboard : IMasterDetailPageProvider, IInitializeAware
    {
        public Dashboard()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.Dashboard();

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

        public IMasterDetailPageManager PageManager { get; set; }

        public void Initialize(IParametersService parameters)
        {
            (BindingContext as ViewModels.Pages.Dashboard)?.PresentPage("About");
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var selectedItem = (MasterPageItem) selectedItemChangedEventArgs.SelectedItem;
            //Detail = PageService.Current.GetCachedOrNewPage(selectedItem.PageKey);
            (BindingContext as ViewModels.Pages.Dashboard)?.PresentPage(selectedItem.PageKey);
            IsPresented = false;
        }
    }
}
