using System;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Services;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class TutorialOne : 
                               IInitializeAware,
                               IPageCachingAware,
                               IPageCachedAware,
                               IPageAppearingAware,
                               IPageAppearedAware,
                               IPageDisappearingAware,
                               IPageDisappearedAware
    {
        public TutorialOne()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Pages.TutorialOne();
            IdLabel.Text = Guid.NewGuid().ToString();
        }

        public void OnPageAppearing(IParametersService parameters)
        {
            var id = parameters.TryGet<int>("Id");
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            //var stack = NavigationService.Current.NavigationStack;
            //NavigationService.Current.PushAsync("TestPage");
            Navigation.PushAsync(new TestPage());
        }

        public void Initialize(IParametersService parameters)
        {
            
        }

        public void OnPageCaching()
        {

        }

        public void OnPageCached()
        {

        }

        public void OnPageAppeared(IParametersService parameters)
        {

        }

        public void OnPageDisappearing(IParametersService parameters)
        {

        }

        public void OnPageDisappeared(IParametersService parameters)
        {

        }
    }
}
