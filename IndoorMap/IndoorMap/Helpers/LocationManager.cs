using IndoorMap.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;

namespace IndoorMap.Helpers
{
    public class LocationManager
    {
        public static async Task<Geoposition> GetPosition()
        {
            //request the access of geolocation
            try
            {
                GeolocationAccessStatus status = await Geolocator.RequestAccessAsync();
                if (status != GeolocationAccessStatus.Allowed)
                {
                    //the location is not opened
                    await new MessageDialog("定位失败! 如有需要，可前往设置打开定位").ShowAsync();
                    //use the baidu map api
                    return null;
                }
                else
                {
                    Geolocator locator = new Geolocator()
                    {
                        DesiredAccuracy = PositionAccuracy.Default,
                        DesiredAccuracyInMeters = 5,
                    };
                    var position = await locator.GetGeopositionAsync();
                    return position;

                }
            }
            catch (Exception e)
            {
                await new MessageDialog("定位失败! 如有需要，可前往设置打开定位").ShowAsync();
                return null;
            }
        }
    }
}
