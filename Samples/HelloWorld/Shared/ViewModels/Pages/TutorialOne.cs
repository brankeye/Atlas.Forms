using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Interfaces.Services;

namespace atlas.samples.helloworld.Shared.ViewModels.Pages
{
    public class TutorialOne : IInitializeAware,
                               IPageCachingAware,
                               IPageCachedAware,
                               IPageAppearingAware,
                               IPageAppearedAware,
                               IPageDisappearingAware,
                               IPageDisappearedAware
    {
        public void Initialize(IParametersService parameters)
        {

        }

        public void OnPageCaching()
        {

        }

        public void OnPageCached()
        {

        }

        public void OnPageAppearing(IParametersService parameters)
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
