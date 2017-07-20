using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using FeatureDemo.Core.Events;
using FeatureDemo.Core.Models;
using FeatureDemo.Core.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Xamarin.Forms;

namespace FeatureDemo.Core.ViewModels
{
    public class ItemsPageViewModel : BaseViewModel, INavigationAware
    {
        INavigationService _navigationService;
        public ObservableRangeCollection<Item> Items { get; set; }

        Item _selectedItem; public Item SelectedItem
        {
            get => _selectedItem; 
            set => SetProperty(ref _selectedItem, value);
        }

        public DelegateCommand LoadItemsCommand { get; private set; }
        public DelegateCommand AddItemCommand { get; private set; }
        public DelegateCommand<Item> OnItemSelectedCommand { get; private set; }

        public ItemsPageViewModel(INavigationService navigationService, IEventAggregator eventAgg)
        {
            _navigationService = navigationService;
            Title = "Browse";
            Items = new ObservableRangeCollection<Item>();
            LoadItemsCommand = new DelegateCommand(async () => await ExecuteLoadItemsCommand());
            AddItemCommand = new DelegateCommand(AddItem);
            OnItemSelectedCommand = new DelegateCommand<Item>(OnItemSelected);

            eventAgg.GetEvent<SaveItemEvent>().Subscribe( async (item) => {
				Items.Add(item);
				await DataStore.AddItemAsync(item);
            });

            //MessagingCenter.Subscribe<NewItemPageViewModel, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    Items.Add(item);
            //    await DataStore.AddItemAsync(item);
            //});
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

        async void AddItem()
        {
            await _navigationService.NavigateAsync("NewItem");
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

        public async void OnItemSelected(Item item)
        {
            var p = new NavigationParameters();
            p.Add("item", item);
            await _navigationService.NavigateAsync("ItemDetail",p);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            //switch(args.PropertyName)
            //{
            //    case "SelectedItem":
            //        if(SelectedItem != null)
            //        {
            //            var p = new NavigationParameters();
            //            p.Add("item", SelectedItem);
            //            _navigationService.NavigateAsync("Navigation/Items/ItemDetail");
            //        }
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
