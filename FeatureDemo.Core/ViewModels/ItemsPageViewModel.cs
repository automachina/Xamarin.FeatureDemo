using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using FeatureDemo.Core.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace FeatureDemo.Core.ViewModels
{
    public class ItemsViewModel : BaseViewModel, INavigationAware
    {
        INavigationService _navigationService;
        public ObservableRangeCollection<Item> Items { get; set; }

        Item _selectedItem; public Item SelectedItem
        {
            get => _selectedItem; 
            set => SetProperty(ref _selectedItem, value);
        }

        public DelegateCommand LoadItemsCommand { get; set; }
        public DelegateCommand AddItemCommand { get; set; }

        public ItemsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Title = "Browse";
            Items = new ObservableRangeCollection<Item>();
            LoadItemsCommand = new DelegateCommand(async () => await ExecuteLoadItemsCommand());
            AddItemCommand = new DelegateCommand(AddItem);

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Item;
                Items.Add(_item);
                await DataStore.AddItemAsync(_item);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }

        void AddItem()
        {
            _navigationService.NavigateAsync("NewItemPage");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if(Items.Count == 0)
            {
                LoadItemsCommand.Execute();   
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            switch(args.PropertyName)
            {
                case "SelectedItem":
                    if(SelectedItem != null)
                    {
                        var p = new NavigationParameters();
                        p.Add("item", SelectedItem);
                        _navigationService.NavigateAsync("NavigationPage/ItemPage/ItemDetailPage");
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
