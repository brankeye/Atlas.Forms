using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces;
using Xamarin.Forms;

namespace Library.Tests.Mocks
{
    public class ApplicationProviderMock : IApplicationProvider
    {
        public Page MainPage { get; set; } = new ContentPage {Title = "Test Page", Content = new Label {Text = "Hi"}};
    }
}
