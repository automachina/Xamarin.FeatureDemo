using System;
using System.Collections.Generic;
using System.Windows.Input;
using FeatureDemo.Core.Models;
using Prism.Commands;
using Prism.Navigation;

namespace FeatureDemo.Core.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        // DelegateCommand's need to be public properties with at least a getter
        public DelegateCommand<string> NavigateCommand { get; set; }

        INavigationService _navigationService;

        List<MasterPageItem> _menuItems; public List<MasterPageItem> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        public MenuPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Title = "Features Menu";
            MenuItems = new List<MasterPageItem>()
            {
                new MasterPageItem("Browse Items", "profile_generic.png", "Navigation/Items"),
                new MasterPageItem("About", OnPlatform("tab_about.png","about.png"), "About")
            };
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        public async void Navigate(string targetUrl)
        {    
            await _navigationService.NavigateAsync(targetUrl);
        }
    }
}
