using System;
using Atlas.Forms.Interfaces.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Components
{
    public interface IPubSubService : IPublisher, ISubscriber
    {
        
    }

    public interface IPublisher
    {
        void SendPageInitializeMessage(Page page, IParametersService parameters);

        void SendPageAppearingMessage(Page page, IParametersService parameters);

        void SendPageAppearedMessage(Page page, IParametersService parameters);

        void SendPageDisappearingMessage(Page page, IParametersService parameters);

        void SendPageDisappearedMessage(Page page, IParametersService parameters);

        void SendPageCachingMessage(Page page);

        void SendPageCachedMessage(Page page);

        void SendPageCreatedMessage(Page page);

        void SendPageNavigatedFromMessage(Page page);

        void SendPageNavigatedToMessage(Page page);
    }

    public interface ISubscriber
    { 
        void SubscribePageInitialize(Action<Page, IParametersService> action);

        void SubscribePageAppearing(Action<Page, IParametersService> action);

        void SubscribePageAppeared(Action<Page, IParametersService> action);

        void SubscribePageDisappearing(Action<Page, IParametersService> action);

        void SubscribePageDisappeared(Action<Page, IParametersService> action);

        void SubscribePageCaching(Action<Page> action);

        void SubscribePageCached(Action<Page> action);

        void SubscribePageCreated(Action<Page> action);

        void SubscribePageNavigatedFrom(Action<Page> action);

        void SubscribePageNavigatedTo(Action<Page> action);

        void UnsubscribePageInitialize();

        void UnsubscribePageAppearing();

        void UnsubscribePageAppeared();

        void UnsubscribePageDisappearing();

        void UnsubscribePageDisappeared();

        void UnsubscribePageCaching();

        void UnsubscribePageCached();

        void UnsubscribePageCreated();

        void UnsubscribePageNavigatedFrom();

        void UnsubscribePageNavigatedTo();
    }
}
