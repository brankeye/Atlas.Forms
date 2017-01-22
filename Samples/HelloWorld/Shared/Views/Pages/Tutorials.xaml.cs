using atlas.core.Library.Services;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class Tutorials
    {
        public Tutorials()
        {
            InitializeComponent();
            Children.Add(PageCacheService.Current.GetCachedOrNewPage("TutorialOne"));
            Children.Add(PageCacheService.Current.GetCachedOrNewPage("TutorialTwo"));
            Children.Add(PageCacheService.Current.GetCachedOrNewPage("TutorialThree"));
        }
    }
}
