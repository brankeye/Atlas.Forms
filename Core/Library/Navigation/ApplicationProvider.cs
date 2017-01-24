using Atlas.Forms.Interfaces;
using Xamarin.Forms;

namespace Atlas.Forms.Navigation
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
