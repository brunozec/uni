using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LojaDeJogos.ViewModels
{
    public class GPSViewModel : BaseViewModel
    {
        private string _latitude;
        private string _longitude;
        private string _altitude;

        public string Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        public string Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        public string Altitude
        {
            get => _altitude;
            set => SetProperty(ref _altitude, value);
        }

        public GPSViewModel()
        {
            Title = "Geo-localização";
            LoadGeoLocationCommand = new Command(async () =>
            {
                //https://learn.microsoft.com/en-us/xamarin/essentials/geolocation?tabs=android
                try
                {
                    //tenta carregar a ultima localização do smartphone
                    var location = await Geolocation.GetLastKnownLocationAsync();

                    if (location != null)
                    {
                        Latitude = location.Latitude.ToString();
                        Longitude = location.Longitude.ToString();
                        Altitude = location.Altitude.ToString();
                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Handle not supported on device exception
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    // Handle not enabled on device exception
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                }
                catch (Exception ex)
                {
                    // Unable to get location
                }

            });
        }

        public ICommand LoadGeoLocationCommand { get; }
    }
}