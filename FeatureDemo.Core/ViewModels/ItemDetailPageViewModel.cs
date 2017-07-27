using FeatureDemo.Core.Models;
using Prism.Navigation;

namespace FeatureDemo.Core.ViewModels
{
    public class ItemDetailPageViewModel : BaseViewModel
    {
        Item _item; public Item Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }
        public ItemDetailPageViewModel(INavigationService navigationService)
            :base(navigationService) { }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if(parameters.TryGetValue("item", out _item))
            {
                Title = _item.Text;
                RaisePropertyChanged("Item");
            }
        }
    }
}
