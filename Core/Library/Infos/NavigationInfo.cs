namespace Atlas.Forms.Infos
{
    public class NavigationInfo
    {
        public NavigationInfo() { }

        public NavigationInfo(string key)
        {
            Page = key;
        }

        public string Page { get; set; }

        public string WrapperPage { get; set; }

        public bool HasWrapperPage { get; set; }

        public bool NewInstanceRequested { get; set; }
    }

    public class NavigationInfoFluent
    {
        private readonly NavigationInfo _navigationInfo;

        public NavigationInfoFluent(NavigationInfo navigationInfo)
        {
            _navigationInfo = navigationInfo;
        }

        public virtual NavigationInfoFluent As(string key)
        {
            _navigationInfo.HasWrapperPage = true;
            _navigationInfo.WrapperPage = key;
            return this;
        }

        public virtual NavigationInfoFluent As<TClass>()
        {
            return As(typeof(TClass).Name);
        }

        public virtual NavigationInfoFluent AsNavigationPage()
        {
            return As("NavigationPage");
        }

        public virtual NavigationInfoFluent AsNewInstance()
        {
            _navigationInfo.NewInstanceRequested = true;
            return this;
        }

        public virtual NavigationInfo Info()
        {
            return _navigationInfo;
        }
    }
}
