namespace atlas.samples.helloworld.Shared.Testers
{
    public class PageMethodTester
    {
        public static PageMethodTester MyContentPageTester { get; set; } = new PageMethodTester();

        public static PageMethodTester MyContentPageViewModelTester { get; set; } = new PageMethodTester();

        public bool InitializeWasCalled { get; set; }

        public bool InitializeWasCalledOnlyOnce { get; set; }

        public bool InitializeParametersNotNull { get; set; }

        public bool OnPageAppearingWasCalled { get; set; }

        public bool AppearingParametersNotNull { get; set; }

        public bool OnPageAppearedWasCalled { get; set; }

        public bool AppearedParametersNotNull { get; set; }

        public bool OnPageDisappearingWasCalled { get; set; }

        public bool DisappearingParametersNotNull { get; set; }

        public bool OnPageDisappearedWasCalled { get; set; }

        public bool DisappearedParametersNotNull { get; set; }

        public bool OnCachingWasCalled { get; set; }

        public bool OnCachedWasCalled { get; set; }
    }
}
