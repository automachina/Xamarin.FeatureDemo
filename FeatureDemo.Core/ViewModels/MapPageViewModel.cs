using System;
using System.Windows.Input;
using Plugin.Geolocator;
using Geo = Plugin.Geolocator.Abstractions;
using TK.CustomMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Diagnostics;
using Prism.Commands;

namespace FeatureDemo.Core.ViewModels
{
    public class MapPageViewModel : BaseViewModel
    {
        Geo.IGeolocator geoloc;
        public DelegateCommand<object> MapClickedCommand { get; set; }
        public DelegateCommand<object> MapPropertyChangedCommand { get; set; }

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

        double _zoomMin; public double ZoomMin
        {
            get => _zoomMin;
            set => SetProperty(ref _zoomMin, value);
        }

		double _zoomMid; public double ZoomMid
		{
			get => _zoomMid;
			set => SetProperty(ref _zoomMid, value);
		}

		double _zoomMax; public double ZoomMax
		{
			get => _zoomMax;
			set => SetProperty(ref _zoomMax, value);
		}

        double _zoomLevel; public double ZoomLevel
        {
            get => _zoomLevel;
            set
            {
                if (value >= _zoomMin && value <= _zoomMax && CurrentRegion != null)
                {
                    ZoomValue = QuadradicZoom(value);
                    CurrentRegion = MapSpan.FromCenterAndRadius(CurrentRegion.Center, Distance.FromKilometers(ZoomValue));
					SetProperty(ref _zoomLevel, value);
                }
            }
        }

        double _zoomValue; public double ZoomValue
        {
            get => _zoomValue;
            set => SetProperty(ref _zoomValue, value);
        }

        MapSpan _currentRegion; public MapSpan CurrentRegion
        {
            get => _currentRegion;
            set => SetProperty(ref _currentRegion, value);
        }

		MapSpan _visibleRegion; public MapSpan VisibleRegion
		{
			get => _visibleRegion;
            set => SetProperty(ref _visibleRegion, value);
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
            MapClickedCommand = new DelegateCommand<object>(MapClicked);
            MapPropertyChangedCommand = new DelegateCommand<object>(MapPropertyChanged);
            CurrentLocation = new Position(geoPosition.Latitude, geoPosition.Longitude);
            CurrentRegion = new MapSpan(CurrentLocation, .08, .08);
			ZoomMin = 0;
            ZoomMid = 10;
			ZoomMax = 500;
			ZoomLevel = 0.5;
            Debug.WriteLine($"CurrentRegion.Radius: {CurrentRegion.Radius.Meters}");
        }

        public void MapClicked(object position)
        {
            if(position is Position)
                CurrentRegion = MapSpan.FromCenterAndRadius((Position)position, CurrentRegion.Radius);
        }

        public void MapPropertyChanged(object map)
        {
            var prop = (TKCustomMap)map;
        }

		// Convert linear scale to quadradic scale
		double QuadradicZoom(double value)
        {
			var a = (_zoomMin * _zoomMax - Math.Pow(_zoomMid, 2)) / (_zoomMin - 2 * _zoomMid + _zoomMax);
			var b = Math.Pow((_zoomMid - _zoomMin), 2) / (_zoomMin - 2 * _zoomMid + _zoomMax);
			var c = 2 * Math.Log((_zoomMax - _zoomMid) / (_zoomMid - _zoomMin));
			return b * (Math.Exp(c * value) - 1.0);
        }

        public override void OnNavigatedTo(Prism.Navigation.NavigationParameters parameters)
        {
            
        }
    }
}
