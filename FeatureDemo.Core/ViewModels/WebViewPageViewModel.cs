using System;
using Prism.Commands;

namespace FeatureDemo.Core.ViewModels
{
    public class WebViewPageViewModel : BaseViewModel
    {
        public DelegateCommand NavigatingCommand { get; set; }
        public DelegateCommand NavigatedCommand { get; set; }

        Uri url; public Uri Url
        {
            get => url;
            set => SetProperty(ref url, value);
        }

        bool isLoading; public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }

        public WebViewPageViewModel()
        {
            IsLoading = false;
            NavigatingCommand = new DelegateCommand(Navigating);
            NavigatedCommand = new DelegateCommand(Navigated);
        }

        public void Navigating()
        {
            IsLoading = true;
        }

        public void Navigated()
        {
            IsLoading = false;
        }

        public override void OnNavigatedTo(Prism.Navigation.NavigationParameters parameters)
        {
            string urlString;
            if(parameters.ContainsKey("url"))
            {
                urlString = parameters.GetValue<string>("url");
                if(Uri.TryCreate(urlString, UriKind.Absolute, out url))
                {
                    RaisePropertyChanged("Url");    
                }

            }

            if(parameters.ContainsKey("title"))
            {
                Title = parameters.GetValue<string>("title");
            }
        }
    }
}
