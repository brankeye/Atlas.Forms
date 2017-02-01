using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages.NavigationPages
{
    public class MyNavigationContentPage : NavigationPage
    {
        public MyNavigationContentPage() : base(new MyContentPage())
        {
            Title = "MyContentPage";
        }
    }
}
