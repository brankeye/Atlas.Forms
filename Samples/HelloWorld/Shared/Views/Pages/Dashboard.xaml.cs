using System.Collections.Generic;
using atlas.core.Library.Extensions;
using atlas.core.Library.Services;
using atlas.samples.helloworld.Shared.ViewModels;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class Dashboard
    {
        public Dashboard()
        {
            InitializeComponent();
            
            //NavigationService.Current.Present();
            this.Present("NavigationPage/Tutorials");

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
            var enumerator = listView.ItemsSource.GetEnumerator();
            if (enumerator.MoveNext()) listView.SelectedItem = enumerator.Current;

            listView.ItemSelected += ListView_OnItemSelected;
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var selectedItem = (MasterPageItem) selectedItemChangedEventArgs.SelectedItem;
            this.Present(selectedItem.PageKey);
        }
    }
}
