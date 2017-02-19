using Atlas.Forms.Infos;
using Atlas.Forms.Services;

namespace atlas.samples.helloworld.Shared.ViewModels
{
    public class MasterPageItem
    {
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public NavigationInfo PageInfo { get; set; }
    }
}
