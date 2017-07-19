using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;

namespace FeatureDemo.Core.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        INavigationService _navigationService;

		/// <summary>
		/// Command to open browser to xamarin.com
		/// </summary>
		public DelegateCommand OpenWebCommand { get; private set; }

        public AboutViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Title = "About";

            OpenWebCommand = new DelegateCommand(async () => await NavigateToWebpage("https://xamarin.com/platform"));
        }

        Task NavigateToWebpage(string url)
        {
            return _navigationService.NavigateAsync(new Uri(url));
        }
    }
}
