using System;
using Prism.Navigation;

namespace FeatureDemo.Core.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Feature Demo Home";
        }
    }
}
