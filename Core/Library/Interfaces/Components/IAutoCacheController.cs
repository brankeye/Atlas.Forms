using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Components
{
    public interface IAutoCacheController
    {
        void Subscribe();

        void Unsubscribe();

        void OnPageNavigatedFrom(Page page);

        void OnPageAppeared(Page page);

        void OnPageDisappeared(Page page);

        void OnPageCreated(Page page);
    }
}
