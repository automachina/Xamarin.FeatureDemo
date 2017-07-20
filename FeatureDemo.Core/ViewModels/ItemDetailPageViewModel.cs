using FeatureDemo.Core.Models;
using Prism.Navigation;

namespace FeatureDemo.Core.ViewModels
{
    public class ItemDetailPageViewModel : BaseViewModel, INavigationAware
    {
        INavigationService _navigationService;

        Item _item; public Item Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }
        public ItemDetailPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if(parameters.TryGetValue("item", out _item)){
                RaisePropertyChanged("Item");
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
