using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace FeatureDemo.Core.ViewModels
{
    public class RootPageViewModel : BaseViewModel
    {
        ContentPage _master; public ContentPage Master 
        {
            get => _master;
            private set => SetProperty(ref _master, value); 
        }

        ContentPage _detail; public ContentPage Detail
		{
			get => _master;
            private set => SetProperty(ref _detail, value);
		}

        bool _isPresented; public bool IsPresented
        {
            get => _isPresented;
            set => SetProperty(ref _isPresented, value);
        }

        public RootPageViewModel()
        {
            Title = "Feature Demo";
            Master = new ContentPage() { Title = "Master" };
            Detail = new ContentPage() { Title = "Detail" };
        }

        public override void OnNavigatedTo(Prism.Navigation.NavigationParameters parameters)
        {
            if(parameters.ContainsKey("master"))
            {
                Master = parameters["master"] as ContentPage;
            }

			if (parameters.ContainsKey("detail"))
			{
                Detail = parameters["detail"] as ContentPage;
			}
        }
    }
}
