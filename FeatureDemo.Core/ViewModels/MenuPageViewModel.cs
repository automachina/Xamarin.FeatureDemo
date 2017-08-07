using System;
using System.Collections.Generic;
using System.Windows.Input;
using FeatureDemo.Core.Helpers;
using FeatureDemo.Core.Models;
using Prism.Commands;
using Prism.Navigation;

namespace FeatureDemo.Core.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        // DelegateCommand's need to be public properties with at least a getter
        public DelegateCommand<string> NavigateCommand { get; set; }

        private Nav Nav { get; }

        private bool _canNavigate; public bool CanNavigateProp
        {
            get => _canNavigate;
            set => SetProperty(ref _canNavigate, value);
        }

        List<MasterPageItem> _menuItems; public List<MasterPageItem> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        public MenuPageViewModel(INavigationService navigationService, Nav nav)
            :base(navigationService)
        {
            Nav = nav;
            Title = "Features Menu";
            MenuItems = new List<MasterPageItem>()
            {
                new MasterPageItem("Browse Items", "profile_generic.png", Nav.To.Items().Go),
                new MasterPageItem("About", OnPlatform("tab_about.png","icon.png"), Nav.To.About("Message=Learn more...").Go),
                new MasterPageItem("Search Repos", "github-logo.png", Nav.To.RepoSearch().Go),
                new MasterPageItem("Find ATM's", "atm_pin.png", Nav.To.Map().Go)
            };
            NavigateCommand = new DelegateCommand<string>(Navigate).ObservesCanExecute(() => CanNavigateProp);
            CanNavigateProp = true;
        }


        public async void Navigate(string route)
        {
            CanNavigateProp = false;
            await _navigationService.NavigateAsync(route, null, false, true);
            CanNavigateProp = true;
        }
    }
}
