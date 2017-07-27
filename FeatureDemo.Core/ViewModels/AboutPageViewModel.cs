using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FeatureDemo.Core.Helpers;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace FeatureDemo.Core.ViewModels
{
    public class AboutPageViewModel : BaseViewModel
    {
        Nav _nav;
		/// <summary>
		/// Command to open browser to xamarin.com
		/// </summary>
        public DelegateCommand OpenWebViewCommand { get; private set; }
        public DelegateCommand OpenWebPageCommand { get; private set; }

        string XamarinUrl = "https://developer.xamarin.com/";
        string _message; public string Message 
        { 
            get => _message; 
            private set => SetProperty(ref _message, value); 
        }

        public AboutPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "About";
            Message = "Link to Xamarin.";
            OpenWebViewCommand = new DelegateCommand(NavigateToWebView);
            OpenWebPageCommand = new DelegateCommand(NavigateToWebPage);
            _nav = new Nav();
        }

        async void NavigateToWebPage()
        {
            var uri = new Uri(XamarinUrl, UriKind.Absolute);
            switch(Device.RuntimePlatform)
            {
                case "iOS":
                    Device.OpenUri(uri);
                    break;
                case "Android":
                    await Task.Run(() => Device.OpenUri(uri));
                    break;
            }
        }

        async void NavigateToWebView()
        {
            Debug.WriteLine(_nav.To.WebView($"url={XamarinUrl}&title=Xamarin Developer Portal").Go);
            await _navigationService.NavigateAsync(_nav.To.WebView($"url={XamarinUrl}&title=Xamarin Developer Portal").Go);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if(parameters.ContainsKey("Message"))
            {
                Message = parameters.GetValue<string>("Message");
            }
        }
    }
}
