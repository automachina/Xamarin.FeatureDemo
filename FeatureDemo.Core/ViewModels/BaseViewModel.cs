using System;
using Xamarin.Forms;
using Prism.Mvvm;
using FeatureDemo.Core.Models;
using Prism.Unity;
using Prism.Navigation;
using System.Threading.Tasks;

namespace FeatureDemo.Core.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware, IConfirmNavigationAsync, IConfirmNavigation, IDestructible
    {

        protected INavigationService _navigationService { get; }

        public BaseViewModel() {}

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;    
        }

        /// <summary>
        /// Get the azure service instance
        /// </summary>
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public T OnPlatform<T>(T iOSValue, T AndroidValue)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return iOSValue;
                case Device.Android:
                    return AndroidValue;
                default:
                    return default(T);
            }
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        public virtual Task<bool> CanNavigateAsync(NavigationParameters parameters)
        {
            return Task.FromResult(true);
        }

        public bool CanNavigate(NavigationParameters parameters)
        {
            return true;
        }

        public virtual void Destroy()
        {
            
        }
    }
}
