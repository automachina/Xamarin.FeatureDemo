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
    public class ItemsPageViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Item> Items { get; set; }

        Item _selectedItem; public Item SelectedItem
        {
            get => _selectedItem; 
            set => SetProperty(ref _selectedItem, value);
        }

        IEventAggregator _eventAgg;

        public DelegateCommand LoadItemsCommand { get; private set; }
        public DelegateCommand AddItemCommand { get; private set; }
        public DelegateCommand<Item> ItemSelectedCommand { get; private set; }
        public DelegateCommand<Item> DeleteItemCommand { get; private set; }
        public DelegateCommand<Item> ItemTappedCommand { get; private set; }

        public ItemsPageViewModel(INavigationService navigationService, IEventAggregator eventAgg)
            :base(navigationService)
        {
            Title = "Browse";
            _eventAgg = eventAgg;
            Items = new ObservableRangeCollection<Item>();
            LoadItemsCommand = new DelegateCommand(ExecuteLoadItemsCommand);
            AddItemCommand = new DelegateCommand(AddItem);
            ItemSelectedCommand = new DelegateCommand<Item>(ItemSelected);
            ItemTappedCommand = new DelegateCommand<Item>(ItemTapped);
            DeleteItemCommand = new DelegateCommand<Item>(DeleteItem);

            _eventAgg.GetEvent<AddItemEvent>().Subscribe(async (item) =>
            {
                Items.Add(item);
                await DataStore.AddItemAsync(item);
            });

            _eventAgg.GetEvent<UpdateItemEvent>().Subscribe(async (item) =>
            {
                if (Items.Contains(item))
                {
                    Items.Replace(item);
                    await DataStore.UpdateItemAsync(item);
                }
            });

            _eventAgg.GetEvent<DeleteItemEvent>().Subscribe(async (item) =>
            {
                Items.Remove(item);
                await DataStore.DeleteItemAsync(item.Id);
            });
        }

        private async void ItemTapped(Item item)
        {
			if (item == null)
				return;

			var p = new NavigationParameters();
			p.Add("item", item);
			await _navigationService.NavigateAsync("ItemDetail", p);
        }

        public async void ItemSelected(Item item)
		{
			if (item == null)
				return;

			var p = new NavigationParameters();
			p.Add("item", item);
			await _navigationService.NavigateAsync("ItemDetail", p);
		}

        public async void DeleteItem(Item item)
        {
            await Task.Run(() => _eventAgg.GetEvent<DeleteItemEvent>().Publish(item));
        }

        public async void ExecuteLoadItemsCommand()
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

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if(Items.Count == 0)
            {
                LoadItemsCommand.Execute();   
            }
        }
    }
}
