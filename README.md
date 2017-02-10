# Atlas.Forms
Atlas is a small library that provides viewmodel-first navigation, event-based automatic page-caching, navigation-stack saving, and a few other useful services.

NOTE: Atlas is still in beta.

## Nuget
Atlas.Forms is available for [download on Nuget](https://www.nuget.org/packages/Atlas.Forms/).
To install Atlas.Forms, run the following command in the Package Manager Console:

    Install-Package Atlas.Forms -Pre

## Usage
Atlas navigation uses an API similar to the INavigation interface.

### Using Atlas.Forms
In your App class, inherit from ```AtlasApplication``` instead of ```Application```.

```csharp
public class App : AtlasApplication {}
```

### Register pages for navigation
In your App class, override ```RegisterPagesForNavigation``` and register pages as needed.

```csharp
protected override void RegisterPagesForNavigation(IPageNavigationRegistry registry)
{
    // Save a reference to the registry if you'd like to use it later.
	registry.RegisterPage<NavigationPage>(); // Key used will be "NavigationPage"
	registry.RegisterPage<Views.Pages.AboutPage>(); // Key used will be "AboutPage"
	registry.RegisterPage<Views.Pages.DashboardPage>("Dashboard"); // Key used will be "Dash"
	
	// With this method you can use ViewModels to navigate, the name of the viewmodel class will be used.
	registry.RegisterPage<ContactPage, ContactViewModel>(); // Key used will be "ContactViewModel"
}
```

### Register pages for caching
In your App class, override ```RegisterPagesForCaching``` and register pages for caching as needed.

```csharp
protected override void RegisterPagesForCaching(IPageCacheRegistry registry)
{
	// When the About page appears the Changelog page will be added to the cache as a Default instance.
	registry.WhenPage("AboutPage").Appears().CachePage("ProfilePage");

	// When the Tutorials page is created it will be added to the cache as a KeepAlive instance.
	registry.WhenPage<TutorialsPage>().IsCreated().CachePage().AsKeepAlive();
	
	// Cache the Contact page when it appears as a SingleInstance.
	registry.WhenPage<Contact>().Appears().CachePage().AsSingleInstance();
	
	// Cache the MyContentPage when it is created and only remove it when the MyMasterDetailPage is removed.
	registry.WhenPage<MyContentPage>().IsCreated().CachePage().AsLifetimeInstance<MyMasterDetailPage>();
}
```

#### There are two triggers that control when pages are automatically cached. These are:

	Appears - When the page appears, cache the chosen page.
	
	IsCreated - When the page is first created, cache the chosen page.

#### There are four options for how page lifetime is handled. These are:

	Default - Removed from cache after first retrieval (means same instance is never used twice).
	
	KeepAlive - Removed from cache when navigating forward or backward.
	
	LifetimeInstance - Removes page X when page Y is removed.
	
	SingleInstance - Never removed from cache. Yours to manage.

### How to navigate
When navigating, the service will first look for a page with the same key in the page-cache, if not found, a new instance will be created.

Set the main page in your App.cs class by making use of the ```Nav``` fluent API helper.

```csharp
public class App : AtlasApplication
{
	public App()
	{
	    var mainPage = Nav.Get<MainMasterDetailPage>().Info();
		NavigationService.SetMainPage(mainPage);
	}
}
```

The ```Nav``` API allows you to get pages via string or class, request a new page instance (cache-instances are retrieved by default), or wrap a page in a NavigationPage (or custom NavigationPage). Some examples:

```csharp
    var firstPage = Nav.Get<FirstPage>().Info();
    // The following uses the "NavigationPage" key to wrap "SecondPage"
    var secondPage = Nav.Get("SecondPage").AsNavigationPage().Info();
    var thirdPage = Nav.Get("ThirdPage").As("CustomNavigationPage").Info();
    // The following will retrieve the FourthPage registered to the FourthPageViewModel
    var fourthPage = Nav.Get<FourthPageViewModel>().AsNewInstance().Info(); 
```


Then use ```PushAsync``` to navigate, with optional parameters to be passed to the ```OnPageAppearing/OnPageAppeared``` functions.
```csharp
public class MainPageViewModel : IPageAppearingAware, IPageAppearedAware, 
                                 INavigationServiceProvider
{
    public INavigationService NavigationService { get; set; }

	private void NavigateButton_OnClicked()
	{
		var parameters = new ParametersService();
		parameters.TryAdd("Id", 1);
		NavigationService.PushAsync(Nav.Get<FirstPage>().Info(), parameters);
	}
	
	public void OnPageAppearing(IParametersService parameters)
	{
		var id = parameters.TryGet<int>("Name"); // Name = "Atlas"
	}
	
	public void OnPageAppeared(IParametersService parameters)
	{
		var id = parameters.TryGet<int>("Name"); // Name = "Atlas"
	}
}

public class NextPageViewModel : 
                INavigationServiceProvider,
                IInitializeAware, 
				IPageAppearingAware, 
				IPageAppearedAware,
				IPageDisappearingAware,
				IPageDisappearedAware,
				IPageCachingAware,
				IPageCachedAware
{
    public INavigationService NavigationService { get; set; }

	private void BackButton_OnClicked()
	{
		var parameters = new ParametersService();
		parameters.TryAdd("Name", "Atlas");
		NavigationService.PopAsync(parameters);
	}

	public void Initialize(IParametersService parameters)
	{
		// Only called once.
		var id = parameters.TryGet<int>("Id"); // Id = 1
	}

	public void OnPageAppearing(IParametersService parameters)
	{
		var id = parameters.TryGet<int>("Id"); // Id = 1
	}
	
	public void OnPageAppeared(IParametersService parameters)
	{
		var id = parameters.TryGet<int>("Id"); // Id = 1
	}
	
	public void OnPageAppearing(IParametersService parameters)
	{
		var id = parameters.TryGet<int>("Id"); // Id = 1}
	}
	
	public void OnPageDisappearing(IParametersService parameters) {}
	
	public void OnPageDisappeared(IParametersService parameters) {}
	
	public void OnPageCaching() {}

    public void OnPageCached() {}
```

### Navigating in a MasterDetailPage

In your view or viewmodel, inherit from ```IMasterDetailPageProvider``` to gain access to the ```PageManager```.

Do not use the PageManager in the view or viewmodel constructor.

```
public class MainMasterDetailPage : IMasterDetailPageProvider, IInitializeAware
{
	public IMasterDetailPageManager PageManager { get; set; }

	public void Initialize(IParametersService parameters)
	{
	    PageManager.PresentPage(Nav.Get("StartingPage").Info());
	}

	public void PresentPage(NavigationInfo navigationInfo)
	{
	    PageManager.PresentPage(navigationInfo);
	}
}
```

### Navigating in a TabbedPage or CarouselPage

In your view or viewmodel, inherit from ```ITabbedPageProvider``` or ```ICarouselPageProvider``` to gain access to the ```PageManager```.

Do not use the PageManager in the view or viewmodel constructor.

```
public class MyTabbedPage : ITabbedPageProvider, IInitializeAware
{
	public IMultiPageManager PageManager { get; set; }

        public MyTabbedPage()
        {
            InitializeComponent();
        }

        public void Initialize(IParametersService parameters)
        {
            PageManager.AddPage(Nav.Get<FirstTabPage>().Info());
            PageManager.AddPage(Nav.Get<SecondTabPage>().Info());
            PageManager.AddPage(Nav.Get<ThirdTabPage>().Info());
        }
}
```

### How to cache and use multiple Navigation stacks

Navigation stacks are saved if a page is cached for later use. Consider the following scenario:
```
// MainMasterDetailPage is the MainPage
// first Detail page is FirstPage, second Detail page is SettingsPage
MainMasterDetailPage / FirstPage / SecondPage
MainMasterDetailPage / SettingsPage / PermissionsPage
```
If the following cache rules are used:
```
registry.WhenPage<FirstPage>().IsCreated().CachePage().AsLifetimeInstance("MainMasterDetailPage");
registry.WhenPage<SettingsPage>().IsCreated().CachePage().AsLifetimeInstance("MainMasterDetailPage");
```

Then whenever you switch between the Detail pages, the navigation history of each will be saved.

### How to use dialog pages from viewmodels.

```
public class MainPageViewModel
{
	private void DisplayAlert()
	{
		PageDialogService.Current.DisplayAlert("Internet connection failed", "Please try to reconnect", "Ok");
	}
	
	private void DisplayAlert()
	{
		PageDialogService.Current.DisplayAlert("Internet connection failed", "Please try to reconnect", "Ok");
	}
	
	private void DisplayOptionAlert()
	{
		PageDialogService.Current.DisplayAlert("Accept survey?", "It won't take long", "Ok", "Nope");
	}
	
	private void DisplayActionSheet()
	{
		PageDialogService.Current.DisplayActionSheet("Save photo", "Cancel", "Delete", "Photo Roll", "Email");
	}
}
```

## Navigation Service API
The Atlas.Forms Navigation API follows the Xamarin.Forms INavigation interface as closely as possible.

Two extra functions make an appearance. The Present function is used to present Detail pages in a MasterDetailPage, while
the SetMainPage function is used to completely switch out the current MainPage.

Use the ParametersService to pass a dictionary of objects to and from pages when navigating forward or backward.

```csharp
public interface INavigationService
{
	IReadOnlyList<IPageContainer> ModalStack { get; }

	IReadOnlyList<IPageContainer> NavigationStack { get; }
	
	INavigation Navigation { get; }

	void InsertPageBefore(string page, string before, IParametersService parameters = null);

	Task<IPageContainer> PopAsync(IParametersService parameters = null);

	Task<IPageContainer> PopAsync(bool animated, IParametersService parameters = null);

	Task<IPageContainer> PopModalAsync(IParametersService parameters = null);

	Task<IPageContainer> PopModalAsync(bool animated, IParametersService parameters = null);

	Task PopToRootAsync();

	Task PopToRootAsync(bool animated);

	Task PushAsync(string page, IParametersService parameters = null);

	Task PushAsync(string page, bool animated, IParametersService parameters = null);

	Task PushModalAsync(string page, IParametersService parameters = null);

	Task PushModalAsync(string page, bool animated, IParametersService parameters = null);

	void RemovePage(string page);

	void SetMainPage(string page, IParametersService parameters = null);
}
```

## Feedback/Bugs/Suggestions
Feel free to add new issues for bugs, suggestions, API requests, etc. 

Feedback is highly appreciated!

## Special thanks to Prism
Prism is a framework for building loosely coupled, maintainable, and testable XAML applications in Xamarin Forms.

If it were not for Prism (headed by Brian Lagunas), this project would not have been possible. I learned a lot from the Prism source code, which I
am grateful for. Thanks again!

See the [repository](https://github.com/PrismLibrary/Prism) on GitHub.

See the [Unity package](https://www.nuget.org/packages/Prism.Unity.Forms) on Nuget.
