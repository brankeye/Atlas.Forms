namespace atlas.samples.helloworld.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new HelloWorld.App());
        }
    }
}
