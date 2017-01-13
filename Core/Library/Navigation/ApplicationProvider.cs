using atlas.core.Library.Interfaces;
using Xamarin.Forms;

namespace atlas.core.Library.Navigation
{
    public class ApplicationProvider : IApplicationProvider
    {
        public Page MainPage
        {
            get { return Application.Current.MainPage; }
            set { Application.Current.MainPage = value; }
        }
    }
}
