using atlas.samples.helloworld.Shared.Testers;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Interfaces.Services;

namespace atlas.samples.helloworld.Shared.ViewModels.Pages
{
    public class MyContentPage : IInitializeAware,
                                 IPageCachingAware,
                                 IPageCachedAware,
                                 IPageAppearingAware,
                                 IPageAppearedAware,
                                 IPageDisappearingAware,
                                 IPageDisappearedAware
    {
        public void Initialize(IParametersService parameters)
        {
            PageMethodTester.MyContentPageViewModelTester.InitializeWasCalledOnlyOnce = true;
            if (PageMethodTester.MyContentPageViewModelTester.InitializeWasCalled)
            {
                PageMethodTester.MyContentPageViewModelTester.InitializeWasCalledOnlyOnce = false;
            }
            PageMethodTester.MyContentPageViewModelTester.InitializeWasCalled = true;
            PageMethodTester.MyContentPageViewModelTester.InitializeParametersNotNull = parameters != null;
        }

        public void OnPageCaching()
        {
            PageMethodTester.MyContentPageViewModelTester.OnCachingWasCalled = true;
        }

        public void OnPageCached()
        {
            PageMethodTester.MyContentPageViewModelTester.OnCachedWasCalled = true;
        }

        public void OnPageAppearing(IParametersService parameters)
        {
            PageMethodTester.MyContentPageViewModelTester.OnPageAppearingWasCalled = true;
            PageMethodTester.MyContentPageViewModelTester.AppearingParametersNotNull = parameters != null;
        }

        public void OnPageAppeared(IParametersService parameters)
        {
            PageMethodTester.MyContentPageViewModelTester.OnPageAppearedWasCalled = true;
            PageMethodTester.MyContentPageViewModelTester.AppearedParametersNotNull = parameters != null;
        }

        public void OnPageDisappearing(IParametersService parameters)
        {
            PageMethodTester.MyContentPageViewModelTester.OnPageDisappearingWasCalled = true;
            PageMethodTester.MyContentPageViewModelTester.DisappearingParametersNotNull = parameters != null;
        }

        public void OnPageDisappeared(IParametersService parameters)
        {
            PageMethodTester.MyContentPageViewModelTester.OnPageDisappearedWasCalled = true;
            PageMethodTester.MyContentPageViewModelTester.DisappearedParametersNotNull = parameters != null;
        }
    }
}
