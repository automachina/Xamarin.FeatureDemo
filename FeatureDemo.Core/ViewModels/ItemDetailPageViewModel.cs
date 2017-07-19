using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;
using Xamarin.Forms;

namespace FeatureDemo.Core.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel, INavigationAware
    {
        INavigationService _navigationService;

        public Item Item; 
        public ItemDetailViewModel(INavigationService navigationService)
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
            if(parameters.TryGetValue("item", out Item)){
                RaisePropertyChanged("Item");
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
