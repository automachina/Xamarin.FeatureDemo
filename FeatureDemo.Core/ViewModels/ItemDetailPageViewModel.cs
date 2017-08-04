using System;
using System.ComponentModel;
using System.Threading.Tasks;
using FeatureDemo.Core.Events;
using FeatureDemo.Core.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;

namespace FeatureDemo.Core.ViewModels
{
    public class ItemDetailPageViewModel : BaseViewModel
    {
        Item _cachedItem;
        Item _item; public Item Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        bool _isDirty; public bool IsDirty
        {
            get => _isDirty;
            set => SetProperty(ref _isDirty, value);
        }

        bool _editing; public bool Editing
        {
            get => _editing;
            set => SetProperty(ref _editing, value);
        }

        bool _labelsVisible; public bool LabelsVisible
        {
            get => _labelsVisible;
            set => SetProperty(ref _labelsVisible, value);
        }

        string _editToolbarText; public string EditToolbarText
        {
            get => _editToolbarText;
            set => SetProperty(ref _editToolbarText, value);
        }

        IEventAggregator _eventAgg;
        IPageDialogService _dialogService;

        public DelegateCommand EditUpdateItemCommand { get; private set; }

        public ItemDetailPageViewModel(INavigationService navigationService, IEventAggregator eventAgg, IPageDialogService dialogService) 
            :base(navigationService) 
        {
            _eventAgg = eventAgg;
            _dialogService = dialogService;
            EditUpdateItemCommand = new DelegateCommand(EditUpdate);
            EditToolbarText = "Edit";
            Editing = false;
            LabelsVisible = true;
        }

        private void EditUpdate()
        {
            if (!Editing)
            {
                Editing = true;
                LabelsVisible = false;
                EditToolbarText = "Save";
            }
            else
            {
                _eventAgg.GetEvent<UpdateItemEvent>().Publish(Item);
                Editing = false;
                LabelsVisible = true;
                IsDirty = false;
                EditToolbarText = "Edit";
            }
        }

        int quantity = 1;

        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }

        void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IsDirty = true;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if(parameters.TryGetValue("item", out _item))
            {
                Title = _item.Text;
                _cachedItem = _item;
                RaisePropertyChanged("Item");
                _item.PropertyChanged += Item_PropertyChanged;
            }
        }

        public override Task<bool> CanNavigateAsync(NavigationParameters parameters)
        {
            if(IsDirty)
            {
                var answer = _dialogService.DisplayAlertAsync("Unsaved Changes!", $"You have unsaved changes.\nßWould you like to discard these changes?", "Yes", "No");
                return answer;
            }
            return Task.FromResult(true);
        }

        public override async void OnNavigatedFrom(NavigationParameters parameters)
        {
			if (IsDirty)
			{
                var answer = await _dialogService.DisplayAlertAsync("Unsaved Changes!", $"You have unsaved changes.\nWould you like to discard these changes?", "Yes", "No");
                if(!answer)
                {
                    _eventAgg.GetEvent<UpdateItemEvent>().Publish(Item);
                }
                else
                {
                    Item.Description = _cachedItem.Description;
                    Item.Text = _cachedItem.Text;
                }
			}
        }

        public override void Destroy()
        {
            if(_item != null)
            {
                _item.PropertyChanged -= Item_PropertyChanged;
            }
        }
    }
}
