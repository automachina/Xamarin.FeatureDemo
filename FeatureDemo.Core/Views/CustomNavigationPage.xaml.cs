using System;
using System.Collections.Generic;
using System.Diagnostics;
using Prism.Navigation;
using Xamarin.Forms;

namespace FeatureDemo.Core.Views
{
    public partial class CustomNavigationPage : NavigationPage, INavigationPageOptions, IDestructible
    {
        public CustomNavigationPage()
        {
            InitializeComponent();
            this.Popped += Handle_Popped;
            this.Pushed += Handle_Pushed;
        }

        void Handle_Popped(object sender, NavigationEventArgs e)
        {
            Debug.WriteLine($"Popped Page: {e.Page.Title}");
        }

        void Handle_Pushed(object sender, NavigationEventArgs e)
        {
            Debug.WriteLine($"Pushed Page: {e.Page.Title}");
        }

        public bool ClearNavigationStackOnNavigation => false;

        public void Destroy()
        {

        }
    }
}
