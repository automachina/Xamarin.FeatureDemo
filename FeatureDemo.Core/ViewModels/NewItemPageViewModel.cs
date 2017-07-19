using System;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace FeatureDemo.Core.ViewModels
{
    public class NewItemPageViewModel : BaseViewModel, INavigationAware
    {
        INavigationService _navigationService;
        DelegateCommand SaveItemCommand { get; set; }

        public Item Item;

        public NewItemPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        void SaveItem()
        {
			MessagingCenter.Send(this, "AddItem", Item);
            _navigationService.GoBackAsync();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Item = new Item() 
            {
				Text = "Item name",
				Description = "This is a nice description"
            };
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
