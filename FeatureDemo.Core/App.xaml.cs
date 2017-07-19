using System.Collections.Generic;
using FeatureDemo.Core.Views;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Xamarin.Forms;

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

        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
			InitializeComponent();

			if (UseMockDataStore)
				DependencyService.Register<MockDataStore>();
			else
				DependencyService.Register<CloudDataStore>();

            NavigationService.NavigateAsync("Root/Menu");

			//SetMainPage();
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<RootPage>("Root");
            Container.RegisterTypeForNavigation<HomePage>("Home");
            Container.RegisterTypeForNavigation<MenuPage>("Menu");
            Container.RegisterTypeForNavigation<AboutPage>("About");
            Container.RegisterTypeForNavigation<ItemsPage>("Items");
            Container.RegisterTypeForNavigation<ItemDetailPage>("ItemDetail");
            Container.RegisterTypeForNavigation<NewItemPage>("NewItem");
            Container.RegisterTypeForNavigation<LoginPage>("Login");
            Container.RegisterTypeForNavigation<NavigationPage>("Navigation");
            Container.RegisterTypeForNavigation<TabbedPage>("Tabbed");
        }

        public static void SetMainPage()
        {
            if (!UseMockDataStore && !Settings.IsLoggedIn)
            {
                Current.MainPage = new NavigationPage(new LoginPage())
                {
                    BarBackgroundColor = (Color)Current.Resources["Primary"],
                    BarTextColor = Color.White
                };
            }
            else
            {
                GoToMainPage();
            }
        }

        public static void GoToMainPage()
        {
            RootPage = new RootPage();
            NavigationPage = new NavigationPage(new HomePage());

            Current.MainPage = new TabbedPage
            {
                Children = {
                    new NavigationPage(new ItemsPage())
                    {
                        Title = "Browse",
                        //Icon = Device.OnPlatform("tab_feed.png", null, null)
                        Icon = Device.RuntimePlatform == Device.iOS ? "trb_feed.png" : null
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        //Icon = Device.OnPlatform("tab_about.png", null, null)
                        Icon = Device.RuntimePlatform == Device.iOS ? "trb_about.png" : null
                    },
                }
            };
        }
    }
}
