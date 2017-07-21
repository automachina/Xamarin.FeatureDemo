using System;
using Prism.Navigation;

namespace FeatureDemo.Core.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        public HomePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Title = "Feature Demo Home";
        }
    }
}
