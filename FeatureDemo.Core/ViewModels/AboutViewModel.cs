using System;
using Prism.Commands;
using Prism.Navigation;

namespace FeatureDemo.Core.ViewModels
{
    public class AboutPageViewModel : BaseViewModel
    {
        INavigationService _navigationService;

		/// <summary>
		/// Command to open browser to xamarin.com
		/// </summary>
		public DelegateCommand OpenWebCommand { get; private set; }

        string XamarinUrl = "https://xamarin.com/platform";

        public AboutPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Title = "About";

            OpenWebCommand = new DelegateCommand(NavigateToWebpage);
        }

        async void NavigateToWebpage()
        {
            await _navigationService.NavigateAsync(new Uri(XamarinUrl, UriKind.Absolute));
        }
    }
}
