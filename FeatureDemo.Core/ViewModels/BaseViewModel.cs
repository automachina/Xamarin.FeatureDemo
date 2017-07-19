using System;
using Xamarin.Forms;
using Prism.Mvvm;

namespace FeatureDemo.Core.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        /// <summary>
        /// Get the azure service instance
        /// </summary>
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public T OnPlatform<T>(T iOSValue, T AndroidValue)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return iOSValue;
                case Device.Android:
                    return AndroidValue;
                default:
                    return default(T);
            }
        }
    }
}
