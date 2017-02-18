using System;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Interfaces.Utilities;
using Atlas.Forms.Utilities;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class PubSubService : IPubSubService
    {
        public static IPublisher Publisher => Instance.Current;

        public static ISubscriber Subscriber => Instance.Current;

        protected static ILazySingleton<IPubSubService> Instance { get; set; }
            = new LazySingleton<IPubSubService>(() => null);

        protected IMessagingService MessagingService { get; }

        public PubSubService(IMessagingService messagingService)
        {
            MessagingService = messagingService;
        }

        public static void SetCurrent(Func<IPubSubService> func)
        {
            Instance.SetCurrent(func);
        }

        protected string Id => "__CacheEvent";

        protected string OnPageInitialize => nameof(OnPageInitialize) + Id;

        protected string OnPageAppearing => nameof(OnPageAppearing) + Id;

        protected string OnPageAppeared => nameof(OnPageAppeared) + Id;

        protected string OnPageDisappearing => nameof(OnPageDisappearing) + Id;

        protected string OnPageDisappeared => nameof(OnPageDisappeared) + Id;

        protected string OnPageCaching => nameof(OnPageCaching) + Id;

        protected string OnPageCached => nameof(OnPageCached) + Id;

        protected string OnPageNavigatedFrom => nameof(OnPageNavigatedFrom) + Id;

        protected string OnPageNavigatedTo => nameof(OnPageNavigatedTo) + Id;

        protected string OnPageCreated => nameof(OnPageCreated) + Id;

        public virtual void SendPageInitializeMessage(Page page, IParametersService parameters)
        {
            if (page == null) return;
            MessagingService.SendMessage(OnPageInitialize, page, parameters);
        }

        public virtual void SendPageAppearingMessage(Page page, IParametersService parameters)
        {
            if (page == null) return;
            MessagingService.SendMessage(OnPageAppeared, page, parameters);
        }

        public virtual void SendPageAppearedMessage(Page page, IParametersService parameters)
        {
            if (page == null) return;
            MessagingService.SendMessage(OnPageAppeared, page, parameters);
        }

        public virtual void SendPageDisappearingMessage(Page page, IParametersService parameters)
        {
            if (page == null) return;
            MessagingService.SendMessage(OnPageDisappearing, page, parameters);
        }

        public virtual void SendPageDisappearedMessage(Page page, IParametersService parameters)
        {
            if (page == null) return;
            MessagingService.SendMessage(OnPageDisappeared, page, parameters);
        }

        public virtual void SendPageCachingMessage(Page page)
        {
            if (page == null) return;
            MessagingService.SendMessage(OnPageCaching, page);
        }

        public virtual void SendPageCachedMessage(Page page)
        {
            if (page == null) return;
            MessagingService.SendMessage(OnPageCached, page);
        }

        public virtual void SendPageNavigatedFromMessage(Page page)
        {
            if (page == null) return;
            MessagingService.SendMessage(OnPageNavigatedFrom, page);
        }

        public virtual void SendPageNavigatedToMessage(Page page)
        {
            if (page == null) return;
            MessagingService.SendMessage(OnPageNavigatedTo, page);
        }

        public virtual void SendPageCreatedMessage(Page page)
        {
            if (page == null) return;
            MessagingService.SendMessage(OnPageCreated, page);
        }

        public virtual void SubscribePageInitialize(Action<Page, IParametersService> action)
        {
            MessagingService.Subscribe(OnPageInitialize, action);
        }

        public virtual void SubscribePageAppearing(Action<Page, IParametersService> action)
        {
            MessagingService.Subscribe(OnPageAppearing, action);
        }

        public virtual void SubscribePageAppeared(Action<Page, IParametersService> action)
        {
            MessagingService.Subscribe(OnPageAppeared, action);
        }

        public virtual void SubscribePageDisappearing(Action<Page, IParametersService> action)
        {
            MessagingService.Subscribe(OnPageDisappearing, action);
        }

        public virtual void SubscribePageDisappeared(Action<Page, IParametersService> action)
        {
            MessagingService.Subscribe(OnPageDisappeared, action);
        }

        public virtual void SubscribePageCaching(Action<Page> action)
        {
            MessagingService.Subscribe(OnPageCaching, action);
        }

        public virtual void SubscribePageCached(Action<Page> action)
        {
            MessagingService.Subscribe(OnPageCached, action);
        }

        public virtual void SubscribePageNavigatedFrom(Action<Page> action)
        {
            MessagingService.Subscribe(OnPageNavigatedFrom, action);
        }

        public virtual void SubscribePageNavigatedTo(Action<Page> action)
        {
            MessagingService.Subscribe(OnPageNavigatedTo, action);
        }

        public virtual void SubscribePageCreated(Action<Page> action)
        {
            MessagingService.Subscribe(OnPageCreated, action);
        }

        public virtual void UnsubscribePageInitialize()
        {
            MessagingService.Unsubscribe<Page, IParametersService>(OnPageInitialize);
        }

        public virtual void UnsubscribePageAppearing()
        {
            MessagingService.Unsubscribe<Page, IParametersService>(OnPageAppearing);
        }

        public virtual void UnsubscribePageAppeared()
        {
            MessagingService.Unsubscribe<Page, IParametersService>(OnPageAppeared);
        }

        public virtual void UnsubscribePageDisappearing()
        {
            MessagingService.Unsubscribe<Page, IParametersService>(OnPageDisappearing);
        }

        public virtual void UnsubscribePageDisappeared()
        {
            MessagingService.Unsubscribe<Page, IParametersService>(OnPageDisappeared);
        }

        public virtual void UnsubscribePageCaching()
        {
            MessagingService.Unsubscribe<Page>(OnPageCaching);
        }

        public virtual void UnsubscribePageCached()
        {
            MessagingService.Unsubscribe<Page>(OnPageCached);
        }

        public virtual void UnsubscribePageNavigatedFrom()
        {
            MessagingService.Unsubscribe<Page>(OnPageNavigatedFrom);
        }

        public virtual void UnsubscribePageNavigatedTo()
        {
            MessagingService.Unsubscribe<Page>(OnPageNavigatedTo);
        }

        public virtual void UnsubscribePageCreated()
        {
            MessagingService.Unsubscribe<Page>(OnPageCreated);
        }
    }
}
