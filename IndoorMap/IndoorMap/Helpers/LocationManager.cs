using IndoorMap.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI.Popups;

namespace IndoorMap.Helpers
{
    public class LocationManager
    {
        public static async Task<bool> IsLocationCanAccess()
        {
            try
            {            
                //request the access of geolocation
                GeolocationAccessStatus status = await Geolocator.RequestAccessAsync();
                if (status == GeolocationAccessStatus.Allowed)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static async Task<Geoposition> GetPosition(uint accuracyInMeters = 0)
        {
            bool access = await IsLocationCanAccess();
            if (access)
            {
                //if the Location Premission is Access
                Geolocator locator = new Geolocator()
                {
                    DesiredAccuracy = PositionAccuracy.Default,
                    DesiredAccuracyInMeters = accuracyInMeters,
                };
                var position = await locator.GetGeopositionAsync();
                return position;
            }
            else
            {
                await new MessageDialog("定位失败! 如有需要，可前往设置打开定位").ShowAsync();
                return null;
            }
        }

        public static async void GetCityUsingMapLocation()
        {
            Geoposition geoposition = await GetPosition();
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(geoposition.Coordinate.Point);
            var a = result.Locations[0];
        }

         
    }
}
