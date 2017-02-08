using System;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Components
{
    public interface ICachePubSubService : ICachePublisher, ICacheSubscriber
    {
        
    }

    public interface ICachePublisher
    {
        void SendPageAppearedMessage(Page page);

        void SendPageDisappearedMessage(Page page);

        void SendPageCreatedMessage(Page page);

        void SendPageNavigatedFromMessage(Page page);

        void SendPageNavigatedToMessage(Page page);
    }

    public interface ICacheSubscriber
    {
        void SubscribePageAppeared(Action<Page> action);

        void SubscribePageDisappeared(Action<Page> action);

        void SubscribePageCreated(Action<Page> action);

        void SubscribePageNavigatedFrom(Action<Page> action);

        void SubscribePageNavigatedTo(Action<Page> action);

        void UnsubscribePageAppeared();

        void UnsubscribePageDisappeared();

        void UnsubscribePageCreated();

        void UnsubscribePageNavigatedFrom();

        void UnsubscribePageNavigatedTo();
    }
}
