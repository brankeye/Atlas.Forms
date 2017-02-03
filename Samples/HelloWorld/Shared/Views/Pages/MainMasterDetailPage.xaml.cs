using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atlas.samples.helloworld.Shared.ViewModels;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Interfaces.Services;
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
                    PageKey = "NavigationPage/MyContentPage"
                },
                new MasterPageItem
                {
                    Title = "MyTabbedPage",
                    PageKey = "MyTabbedPage"
                }
            };

            listView.ItemsSource = masterPageItems;
            //var enumerator = listView.ItemsSource.GetEnumerator();
            //if (enumerator.MoveNext()) listView.SelectedItem = enumerator.Current;

            listView.ItemSelected += ListView_OnItemSelected;
        }

        public IMasterDetailPageManager PageManager { get; set; }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var selectedItem = (MasterPageItem)selectedItemChangedEventArgs.SelectedItem;
            //Detail = PageService.Current.GetCachedOrNewPage(selectedItem.PageKey);
            (BindingContext as ViewModels.Pages.MainMasterDetailPage)?.PresentPage(selectedItem.PageKey);
            IsPresented = false;
        }
    }
}
