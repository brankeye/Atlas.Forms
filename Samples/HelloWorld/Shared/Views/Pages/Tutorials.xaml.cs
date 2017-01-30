using System;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Services;
using Xamarin.Forms;

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
            Manager.AddPage("NavigationPage/TutorialOne");
            Manager.AddPage("TutorialTwo");
            Manager.AddPage("TutorialThree");
        }

        public IMultiPageManager Manager { get; set; }
    }
}
