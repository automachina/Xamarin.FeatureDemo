﻿using System.Collections.Generic;
using System.Diagnostics;
using FeatureDemo.Core.Helpers;
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

            Debug.WriteLine(Nav.To.Navigation.Root.Menu.Go);

            NavigationService.NavigateAsync(Nav.To.Navigation.Root.Menu.Go);


			//SetMainPage();
        }

        protected override void RegisterTypes()
        {
            Nav = new Nav();
            Container.RegisterTypeForNavigation<RootPage>(Nav.To.Root.Go);
            Container.RegisterTypeForNavigation<HomePage>(Nav.To.Home.Go);
            Container.RegisterTypeForNavigation<MenuPage>(Nav.To.Menu.Go);
            Container.RegisterTypeForNavigation<AboutPage>(Nav.To.About.Go);
            Container.RegisterTypeForNavigation<ItemsPage>(Nav.To.Items.Go);
            Container.RegisterTypeForNavigation<ItemDetailPage>(Nav.To.ItemDetail.Go);
            Container.RegisterTypeForNavigation<NewItemPage>(Nav.To.NewItem.Go);
            Container.RegisterTypeForNavigation<LoginPage>(Nav.To.Login.Go);
            Container.RegisterTypeForNavigation<NavigationPage>(Nav.To.Navigation.Go);
            Container.RegisterTypeForNavigation<TabbedPage>(Nav.To.Tabbed.Go);
            Container.RegisterTypeForNavigation<MasterDetailPage>(Nav.To.MasterDetail.Go);

            Container.RegisterType<INav, Nav>(new ContainerControlledLifetimeManager());
        }

        /*
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
        */
    }
}
