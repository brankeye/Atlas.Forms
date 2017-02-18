using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Interfaces.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class MessagingController : IMessagingController
    {
        protected ISubscriber Subscriber { get; }

        protected IAutoCacheController AutoCacheController { get; }

        protected IPageActionInvoker PageActionInvoker { get; }

        public MessagingController(ISubscriber subscriber,
                                   IAutoCacheController autoCacheController,
                                   IPageActionInvoker pageActionInvoker)
        {
            Subscriber = subscriber;
            AutoCacheController = autoCacheController;
            PageActionInvoker = pageActionInvoker;
            SubscribeInternal();
        }

        private void SubscribeInternal()
        {
            Subscribe();
        }

        public virtual void Subscribe()
        {
            Subscriber.SubscribePageInitialize(OnPageInitialize);
            Subscriber.SubscribePageAppearing(OnPageAppearing);
            Subscriber.SubscribePageAppeared(OnPageAppeared);
            Subscriber.SubscribePageDisappearing(OnPageDisappearing);
            Subscriber.SubscribePageDisappeared(OnPageDisappeared);
            Subscriber.SubscribePageCaching(OnPageCaching);
            Subscriber.SubscribePageCached(OnPageCached);
            Subscriber.SubscribePageNavigatedFrom(OnPageNavigatedFrom);
            Subscriber.SubscribePageCreated(OnPageCreated);
        }

        public void OnPageAppeared(Page page, IParametersService parameters)
        {
            PageActionInvoker.InvokeOnPageAppeared(page, parameters);
            AutoCacheController.OnPageAppeared(page);
        }

        public void OnPageAppearing(Page page, IParametersService parameters)
        {
            PageActionInvoker.InvokeOnPageAppearing(page, parameters);
        }

        public void OnPageCached(Page page)
        {
            PageActionInvoker.InvokeOnPageCached(page);
        }

        public void OnPageCaching(Page page)
        {
            PageActionInvoker.InvokeOnPageCaching(page);
        }

        public void OnPageCreated(Page page)
        {
            AutoCacheController.OnPageCreated(page);
        }

        public void OnPageDisappeared(Page page, IParametersService parameters)
        {
            PageActionInvoker.InvokeOnPageDisappeared(page, parameters);
            AutoCacheController.OnPageDisappeared(page);
        }

        public void OnPageDisappearing(Page page, IParametersService parameters)
        {
            PageActionInvoker.InvokeOnPageDisappearing(page, parameters);
        }

        public void OnPageInitialize(Page page, IParametersService parameters)
        {
            PageActionInvoker.InvokeInitialize(page, parameters);
        }

        public void OnPageNavigatedFrom(Page page)
        {
            AutoCacheController.OnPageNavigatedFrom(page);
        }

        public void Unsubscribe()
        {
            Subscriber.UnsubscribePageInitialize();
            Subscriber.UnsubscribePageAppearing();
            Subscriber.UnsubscribePageAppeared();
            Subscriber.UnsubscribePageDisappearing();
            Subscriber.UnsubscribePageDisappeared();
            Subscriber.UnsubscribePageCaching();
            Subscriber.UnsubscribePageCached();
            Subscriber.UnsubscribePageNavigatedFrom();
            Subscriber.UnsubscribePageCreated();
        }
    }
}
