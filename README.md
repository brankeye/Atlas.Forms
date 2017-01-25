# Atlas.Forms
Atlas is a small library that provides a viewmodel-first navigation service, a page-cache service, automatic page-caching when navigating, as well as a page dialog service, all usable from view or viewmodel.

NOTE: Atlas is still in beta.

## Nuget
Atlas.Forms is available for [download on Nuget](https://www.nuget.org/packages/Atlas.Forms/).
To install Atlas.Forms, run the following command in the Package Manager Console:

    Install-Package Atlas.Forms -Pre

## Usage
Atlas navigation uses an API similar to the INavigation interface, where strings replace Page references.

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
	registry.RegisterPage<Views.Pages.About>(); // Key used will be "About"
	registry.RegisterPage<Views.Pages.Dashboard>("Dash"); // Key used will be "Dash"
}
```

### Register pages for caching
In your App class, override ```RegisterPagesForCaching``` and register pages for caching as needed.

```csharp
protected override void RegisterPagesForCaching(IPageCacheRegistry registry)
{
	// When the About page appears the Changelog page will be added to the cache as a Default instance.
	registry.WhenPage<About>().Appears().CachePage("Changelog");

	// When the Tutorials page is created it will be added to the cache as a KeepAlive instance.
	registry.WhenPage<Tutorials>().IsCreated().CachePage().AsKeepAlive();
	
	// Cache the Contact page when it appears as a SingleInstance.
	registry.WhenPage<Contact>().Appears().CachePage().AsSingleInstance();
}
```

#### There are three triggers that control when pages are automatically cached. These are:

	Appears - When the page appears, cache the chosen page.
	
	Disappears - When the page disappears, cache the chosen page.
	
	IsCreated - When the page is first created, cache the chosen page.

#### There are three options for how page lifetime is handled. These are:

	Default - Removed from cache after first retrieval (means same instance is never used twice).
	
	KeepAlive - Removed from cache when navigating forward or backward (ideal for MasterDetailPage).
	
	SingleInstance - Never removed from cache. Yours to manage.

### How to navigate
Set the main page in your App.cs class.

```csharp
public class App : AtlasApplication
{
	public App()
	{
		NavigationService.Current.SetMainPage("MainPage");
		//NavigationService.Current.SetMainPage("NavigationPage/MainPage");
		//NavigationService.Current.SetMainPage("MyPageContainer/MainPage");
	}
}
```

When navigating, the service will first look for a page with the same key in the page-cache, if not found, a new instance will be created.

All ```MasterDetailPage``` implementations must set the ```Detail``` in the constructor using the ```PageCacheService```.

Then use ```PushAsync``` to navigate, with optional parameters to be passed to the ```OnPageAppearing/OnPageAppeared``` functions.
```csharp
public class MainPageViewModel : IPageAppearingAware, IPageAppearedAware
{
	private void NavigateButton_OnClicked()
	{
		var parameters = new ParametersService();
		parameters.TryAdd("Id", 1);
		NavigationService.Current.PushAsync("NextPage", parameters);
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

public class NextPageViewModel : IInitializeAware, IPageAppearingAware, IPageAppearedAware
{
	private void BackButton_OnClicked()
	{
		var parameters = new ParametersService();
		parameters.TryAdd("Name", "Atlas");
		NavigationService.Current.PopAsync(parameters);
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
}
```

### How to use dialog pages from viewmodels.

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

## API
The Atlas.Forms Navigation API follows the Xamarin.Forms INavigation interface as closely as possible.

Two extra functions make an appearance. The Present function is used to present Detail pages in a MasterDetailPage, while
the SetMainPage function is used to completely switch out the current MainPage.

Use the ParametersService to pass a dictionary of objects to and from pages when navigating forward or backward.

```csharp
public interface INavigationService
{
	IReadOnlyList<IPageContainer> ModalStack { get; }

	IReadOnlyList<IPageContainer> NavigationStack { get; }

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

	void PresentPage(string page, IParametersService parameters = null);

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
