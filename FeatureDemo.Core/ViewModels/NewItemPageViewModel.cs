using FeatureDemo.Core.Events;
using FeatureDemo.Core.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Xamarin.Forms;

namespace FeatureDemo.Core.ViewModels
{
    public class NewItemPageViewModel : BaseViewModel
    {
        public DelegateCommand SaveItemCommand { get; private set; }
        IEventAggregator _eventAgg;

        Item _item; public Item Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        public NewItemPageViewModel(INavigationService navigationService, IEventAggregator eventAgg)
            : base(navigationService)
        {
            _eventAgg = eventAgg;
            Title = "New Item";
            SaveItemCommand = new DelegateCommand(SaveItem);
        }

        async void SaveItem()
        {
            _eventAgg.GetEvent<SaveItemEvent>().Publish(Item);
            await _navigationService.GoBackAsync();
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            Item = new Item() 
            {
				Text = "Item name",
				Description = "This is a nice description"
            };
        }
    }
}
