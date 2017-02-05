using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class MyTabbedPage : ITabbedPageProvider, IInitializeAware
    {
        public MyTabbedPage()
        {
            InitializeComponent();
        }

        public void Initialize(IParametersService parameters)
        {
            PageManager.AddPage(Nav.Get("FirstTabPage").AsNavigationPage().Info());
            PageManager.AddPage(Nav.Get("SecondTabPage").AsNavigationPage().Info());
            PageManager.AddPage(Nav.Get("ThirdTabPage").Info());
        }

        public IMultiPageManager PageManager { get; set; }
    }
}
