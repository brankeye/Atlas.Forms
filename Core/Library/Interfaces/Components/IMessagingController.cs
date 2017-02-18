using Atlas.Forms.Interfaces.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Components
{
    public interface IMessagingController
    {
        void Subscribe();

        void Unsubscribe();

        void OnPageNavigatedFrom(Page page);

        void OnPageInitialize(Page page, IParametersService parameters);

        void OnPageAppearing(Page page, IParametersService parameters);

        void OnPageAppeared(Page page, IParametersService parameters);

        void OnPageDisappearing(Page page, IParametersService parameters);

        void OnPageDisappeared(Page page, IParametersService parameters);

        void OnPageCaching(Page page);

        void OnPageCached(Page page);

        void OnPageCreated(Page page);
    }
}
