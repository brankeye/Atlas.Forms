using System;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Navigation;

namespace Atlas.Forms.Services
{
    public class PageDialogService : IPageDialogService
    {
        public static IPageDialogService Current => GetCurrent();
        private static Lazy<IPageDialogService> _current;

        protected static IPageDialogService GetCurrent()
        {
            if (_current == null)
            {
                _current = new Lazy<IPageDialogService>();
            }
            return _current.Value;
        }

        public static void SetCurrent(Func<IPageDialogService> func)
        {
            _current = new Lazy<IPageDialogService>(func);
        }

        protected IApplicationProvider ApplicationProvider { get; }

        public PageDialogService(IApplicationProvider applicationProvider)
        {
            ApplicationProvider = applicationProvider;
        }

        public virtual Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return ApplicationProvider.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
        }

        public virtual Task DisplayAlert(string title, string message, string cancel)
        {
            return ApplicationProvider.MainPage.DisplayAlert(title, message, cancel);
        }

        public virtual Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return ApplicationProvider.MainPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}
