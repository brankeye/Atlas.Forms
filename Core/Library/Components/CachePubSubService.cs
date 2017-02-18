using System;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Interfaces.Utilities;
using Atlas.Forms.Utilities;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class CachePubSubService : ICachePubSubService
    {
        public static ICachePublisher Publisher => Instance.Current;

        public static ICacheSubscriber Subscriber => Instance.Current;

        protected static ILazySingleton<ICachePubSubService> Instance { get; set; }
            = new LazySingleton<ICachePubSubService>(() => null);

        protected IMessagingService MessagingService { get; }

        public CachePubSubService(IMessagingService messagingService)
        {
            MessagingService = messagingService;
        }

        public static void SetCurrent(Func<ICachePubSubService> func)
        {
            Instance.SetCurrent(func);
        }

        protected string Id => "__CacheEvent";

        protected string OnPageAppearing => nameof(OnPageAppearing) + Id;

        protected string OnPageAppeared => nameof(OnPageAppeared) + Id;

        protected string OnPageDisappearing => nameof(OnPageDisappearing) + Id;

        protected string OnPageDisappeared => nameof(OnPageDisappeared) + Id;

        protected string OnPageCaching => nameof(OnPageCaching) + Id;

        protected string OnPageCached => nameof(OnPageCached) + Id;

        protected string OnPageNavigatedFrom => nameof(OnPageNavigatedFrom) + Id;

        protected string OnPageNavigatedTo => nameof(OnPageNavigatedTo) + Id;

        protected string OnPageCreated => nameof(OnPageCreated) + Id;

        public virtual void SendPageAppearingMessage(Page page)
        {
            if (page == null) return;
            MessagingService.SendMessage(OnPageAppeared, page);
        }

        public virtual void SendPageAppearedMessage(Page page)
        {
            if (page == null) return;
            MessagingService.SendMessage(OnPageAppeared, page);
        }

        public virtual void SendPageDisappearedMessage(Page page)
        {
            if (page == null) return;
            MessagingService.SendMessage(OnPageDisappeared, page);
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

        public virtual void SubscribePageAppeared(Action<Page> action)
        {
            MessagingService.Subscribe(OnPageAppeared, action);
        }

        public virtual void SubscribePageDisappeared(Action<Page> action)
        {
            MessagingService.Subscribe(OnPageDisappeared, action);
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

        public virtual void UnsubscribePageAppeared()
        {
            MessagingService.Unsubscribe<Page>(OnPageAppeared);
        }

        public virtual void UnsubscribePageDisappeared()
        {
            MessagingService.Unsubscribe<Page>(OnPageDisappeared);
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
