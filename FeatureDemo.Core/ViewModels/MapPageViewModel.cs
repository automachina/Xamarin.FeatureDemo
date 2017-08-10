using System;
using System.Windows.Input;
using Plugin.Geolocator;
using Geo = Plugin.Geolocator.Abstractions;
using TK.CustomMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FeatureDemo.Core.ViewModels
{
    public class MapPageViewModel : BaseViewModel
    {
        Geo.IGeolocator geoloc;

        ObservableRangeCollection<TKCustomMapPin> _pins; public ObservableRangeCollection<TKCustomMapPin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        Position _currentLocation; public Position CurrentLocation
        {
            get => _currentLocation;
            set => SetProperty(ref _currentLocation, value);
        }

        public MapPageViewModel()
        {
            geoloc = CrossGeolocator.Current;
            Title = "ATM Map";
            Pins = new ObservableRangeCollection<TKCustomMapPin>(DataStore.GetAtmsAsync().Result);
            var geoPosition = geoloc.GetPositionAsync(new TimeSpan(1000)).Result;
            CurrentLocation = new Position(geoPosition.Latitude, geoPosition.Longitude);
        }

        public override void OnNavigatedTo(Prism.Navigation.NavigationParameters parameters)
        {
            
        }
    }
}
