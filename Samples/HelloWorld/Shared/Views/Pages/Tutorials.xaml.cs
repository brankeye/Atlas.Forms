using Atlas.Forms.Services;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class Tutorials
    {
        public Tutorials()
        {
            InitializeComponent();
            Children.Add(PageService.Current.GetCachedOrNewPage("NavigationPage/TutorialOne"));
            Children.Add(PageService.Current.GetCachedOrNewPage("TutorialTwo"));
            Children.Add(PageService.Current.GetCachedOrNewPage("TutorialThree"));
        }
    }
}
