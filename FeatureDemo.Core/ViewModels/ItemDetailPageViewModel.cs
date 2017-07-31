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

        IEventAggregator _eventAgg;
        IPageDialogService _dialogService;

        public DelegateCommand EditItemCommand { get; private set; }
        public DelegateCommand UpdateItemCommand { get; private set; }

        public ItemDetailPageViewModel(INavigationService navigationService, IEventAggregator eventAgg, IPageDialogService dialogService) 
            :base(navigationService) 
        {
            _eventAgg = eventAgg;
            _dialogService = dialogService;
            EditItemCommand = new DelegateCommand(EditMode);
            UpdateItemCommand = new DelegateCommand(Save);
            Editing = false;
            LabelsVisible = true;
        }

        private void Save()
        {
            _eventAgg.GetEvent<UpdateItemEvent>().Publish(Item);
            IsDirty = false;
        }

        private void EditMode()
        {
            Editing = !Editing;
            LabelsVisible = !LabelsVisible;
        }

        int quantity = 1;

        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            switch(args.PropertyName)
            {
                case "Item":
                    IsDirty = true;
                    break;    
            }
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if(parameters.TryGetValue("item", out _item))
            {
                Title = _item.Text;
                RaisePropertyChanged("Item");
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
    }
}
