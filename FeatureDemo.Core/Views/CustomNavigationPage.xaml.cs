using System;
using System.Collections.Generic;
using Prism.Navigation;
using Xamarin.Forms;

namespace FeatureDemo.Core.Views
{
    public partial class CustomNavigationPage : NavigationPage, INavigationPageOptions, IDestructible
    {
        public CustomNavigationPage()
        {
            InitializeComponent();
        }

        public bool ClearNavigationStackOnNavigation => false;

        public void Destroy()
        {

        }
    }
}
