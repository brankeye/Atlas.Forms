using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Interfaces.Services;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class Tutorials : ITabbedPageProvider, IInitializeAware
    {
        public Tutorials()
        {
            InitializeComponent();
        }

        public void Initialize(IParametersService parameters)
        {
            PageManager.AddPage("NavigationPage/TutorialOne");
            PageManager.AddPage("TutorialTwo");
            PageManager.AddPage("TutorialThree");
        }

        public IMultiPageManager PageManager { get; set; }
    }
}
