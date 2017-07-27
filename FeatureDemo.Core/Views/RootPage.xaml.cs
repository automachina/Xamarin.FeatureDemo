using System;
using System.Collections.Generic;
using Prism.Navigation;
using Xamarin.Forms;

namespace FeatureDemo.Core.Views
{
    public partial class RootPage : MasterDetailPage, IMasterDetailPageOptions
    {
        public RootPage()
        {
            InitializeComponent();
            Master = new MenuPage();
            //Detail = new HomePage();
            MasterBehavior = MasterBehavior.Split;
        }

        public bool IsPresentedAfterNavigation => Device.Idiom != TargetIdiom.Phone;
    }
}
