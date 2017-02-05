using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atlas.samples.helloworld.Shared.Testers;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class MyContentPage : IInitializeAware,
                                         IPageCachingAware,
                                         IPageCachedAware,
                                         IPageAppearingAware,
                                         IPageAppearedAware,
                                         IPageDisappearingAware,
                                         IPageDisappearedAware,
                                         INavigationServiceProvider
    {
        public INavigationService NavigationService { get; set; }

        public MyContentPage()
        {
            InitializeComponent();
            Label.Text = Guid.NewGuid().ToString();
        }

        public void Initialize(IParametersService parameters)
        {
            PageMethodTester.MyContentPageTester.InitializeWasCalledOnlyOnce = true;
            if (PageMethodTester.MyContentPageTester.InitializeWasCalled)
            {
                PageMethodTester.MyContentPageTester.InitializeWasCalledOnlyOnce = false;
            }
            PageMethodTester.MyContentPageTester.InitializeWasCalled = true;
            PageMethodTester.MyContentPageTester.InitializeParametersNotNull = parameters != null;
        }

        public void OnPageCaching()
        {
            PageMethodTester.MyContentPageTester.OnCachingWasCalled = true;
        }

        public void OnPageCached()
        {
            PageMethodTester.MyContentPageTester.OnCachedWasCalled = true;
        }

        public void OnPageAppearing(IParametersService parameters)
        {
            PageMethodTester.MyContentPageTester.OnPageAppearingWasCalled = true;
            PageMethodTester.MyContentPageTester.AppearingParametersNotNull = parameters != null;
        }

        public void OnPageAppeared(IParametersService parameters)
        {
            PageMethodTester.MyContentPageTester.OnPageAppearedWasCalled = true;
            PageMethodTester.MyContentPageTester.AppearedParametersNotNull = parameters != null;
        }

        public void OnPageDisappearing(IParametersService parameters)
        {
            PageMethodTester.MyContentPageTester.OnPageDisappearingWasCalled = true;
            PageMethodTester.MyContentPageTester.DisappearingParametersNotNull = parameters != null;
        }

        public void OnPageDisappeared(IParametersService parameters)
        {
            PageMethodTester.MyContentPageTester.OnPageDisappearedWasCalled = true;
            PageMethodTester.MyContentPageTester.DisappearedParametersNotNull = parameters != null;
        }

        private void Button_OnTestDisplayAlertWithCancel(object sender, EventArgs e)
        {
            PageDialogService.Current.DisplayAlert("Alert", "Here's your message.", "Ok");
        }

        private void Button_OnTestDisplayAlertWithAcceptCancel(object sender, EventArgs e)
        {
            PageDialogService.Current.DisplayAlert("Alert", "Here's your message.", "Ok", "Cancel");
        }

        private void Button_OnTestDisplayActionSheetWithDestruct(object sender, EventArgs e)
        {
            PageDialogService.Current.DisplayActionSheet("Alert", "Cancel", "Delete", "Save", "Edit");
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var history = NavigationService.NavigationStack;
            NavigationService.PushAsync(Nav.Get("MyNextPage").Info());
        }
    }
}
