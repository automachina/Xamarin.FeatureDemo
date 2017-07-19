using System;
using System.Collections.Generic;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;

namespace FeatureDemo.Core.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        public DelegateCommand NavigateCommand;
        INavigationService _navigationService;
        List<MasterPageItem> _masterPages; public List<MasterPageItem> MenuItems
        {
            get => _masterPages;
            set => SetProperty(ref _masterPages, value);
        }

        public MenuPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Title = "Features Menu";
            MenuItems = new List<MasterPageItem>()
            {
                new MasterPageItem("Browse Items", "profile_generic.png", "Items"),
                new MasterPageItem("About", OnPlatform("tab_about.png","about.png"), "About")
            };
        }

        public void Navigate(object targetUrl)
        {
            if (targetUrl is string)
            {
                _navigationService.NavigateAsync((string)targetUrl);
            }
        }
    }
}
