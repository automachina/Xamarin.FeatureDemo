using System.Collections.Generic;
using System.Diagnostics;
using FeatureDemo.Core.Helpers;
using FeatureDemo.Core.Views;
using Microsoft.Practices.Unity;
using Prism.Navigation;
using Prism.Unity;
using Prism.Common;
using Xamarin.Forms;
using XLabs.Serialization;
using XLabs.Serialization.JsonNET;


namespace FeatureDemo.Core
{
    public partial class App : PrismApplication
    {
        public static bool UseMockDataStore = true;
		// Git REST API Docs https://developer.github.com/v3/
		public static string BackendUrl = "https://localhost:5000";

        public static IDictionary<string, string> LoginParameters => null;

        public static NavigationPage NavigationPage { get; private set; }

        public static RootPage RootPage;

        private Nav Nav;

		public static bool MenuIsPresented
		{
			get
			{
				return RootPage.IsPresented;
			}
			set
			{
				RootPage.IsPresented = value;
			}
		}

        public App(IPlatformInitializer initializer = null) : base(initializer) 
        {
            Nav = new Nav();
        }

        protected override void OnInitialized()
        {
			InitializeComponent();

			if (UseMockDataStore)
				DependencyService.Register<MockDataStore>();
			else
				DependencyService.Register<CloudDataStore>();

            NavigationService.NavigateAsync(Nav.To.Navigation().Root().Home().Go, null, false, true);


			//SetMainPage();
        }

        protected override void RegisterTypes()
        {
            Container.RegisterType<Nav>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IJsonSerializer,JsonSerializer>(new ContainerControlledLifetimeManager());
            Nav = Container.Resolve<Nav>();

            Container.RegisterTypeForNavigation<RootPage>(Nav.Root);
            Container.RegisterTypeForNavigation<HomePage>(Nav.Home);
            Container.RegisterTypeForNavigation<MenuPage>(Nav.Menu);
            Container.RegisterTypeForNavigation<AboutPage>(Nav.About);
            Container.RegisterTypeForNavigation<ItemsPage>(Nav.Items);
            Container.RegisterTypeForNavigation<ItemDetailPage>(Nav.ItemDetail);
            Container.RegisterTypeForNavigation<NewItemPage>(Nav.NewItem);
            Container.RegisterTypeForNavigation<LoginPage>(Nav.Login);
            Container.RegisterTypeForNavigation<CustomNavigationPage>(Nav.Navigation);
            Container.RegisterTypeForNavigation<TabbedPage>(Nav.Tabbed);
            Container.RegisterTypeForNavigation<MasterDetailPage>(Nav.MasterDetail);
            Container.RegisterTypeForNavigation<WebViewPage>(Nav.WebView);
            Container.RegisterTypeForNavigation<RepositorySearchPage>(Nav.RepoSearch);
            Container.RegisterTypeForNavigation<MapPage>(Nav.Map);
            Container.RegisterTypeForNavigation<CustomWebViewPage>(Nav.HyperWebView);
        }
    }
}
