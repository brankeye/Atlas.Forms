using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Managers;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class CarouselPageManager : MultiPageManager<ContentPage>, ICarouselPageManager
    {
        public CarouselPageManager(
            CarouselPage page,
            INavigationController navigationController,
            IPageRetriever pageRetriever,
            IPageKeyStore pageKeyStore) 
            : base(page, navigationController, pageRetriever, pageKeyStore)
        {

        }
    }
}
