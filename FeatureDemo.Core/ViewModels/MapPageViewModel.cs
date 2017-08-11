using System;
using System.Windows.Input;
using Plugin.Geolocator;
using Geo = Plugin.Geolocator.Abstractions;
using TK.CustomMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Diagnostics;

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

        float _zoomLevel; public float ZoomLevel
        {
            get => _zoomLevel;
            set
            {
                CurrentRegion = CurrentRegion?.WithZoom(value);
                SetProperty(ref _zoomLevel, value);
            }
        }

        MapSpan _currentRegion; public MapSpan CurrentRegion
        {
            get => _currentRegion;
            set => SetProperty(ref _currentRegion, value);
        }

        public MapPageViewModel()
        {
            geoloc = CrossGeolocator.Current;
            geoloc.DesiredAccuracy = 100;

            Title = "ATM Map";
            Pins = new ObservableRangeCollection<TKCustomMapPin>(DataStore.GetAtmsAsync().Result);
            var geoPosition = new Geo.Position();
            if(false && geoloc.IsGeolocationEnabled && geoloc.IsGeolocationAvailable)
            {
                geoPosition = geoloc.GetPositionAsync().Result;    
            }
            else
            {
                geoPosition = new Geo.Position { Latitude = 44.066795, Longitude = -123.08019 };
            }

            CurrentLocation = new Position(geoPosition.Latitude, geoPosition.Longitude);
            CurrentRegion = new MapSpan(CurrentLocation, .08, .08);
            Debug.WriteLine($"CurrentRegion.Radius: {CurrentRegion.Radius.Meters}");
        }

        public override void OnNavigatedTo(Prism.Navigation.NavigationParameters parameters)
        {
            
        }
    }
}
