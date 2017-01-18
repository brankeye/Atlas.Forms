using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Interfaces.Pages;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class TutorialOne : IPageAppearingAware
    {
        public TutorialOne()
        {
            InitializeComponent();
        }

        public void OnPageAppearing(IParametersService parameters = null)
        {
            
        }
    }
}
