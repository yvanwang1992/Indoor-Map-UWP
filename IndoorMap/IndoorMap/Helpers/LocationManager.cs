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


        #region //World Geodetic System ==> Mars Geodetic System

        const double pi = 3.14159265358979324;
        //
        // Krasovsky 1940
        //
        // a = 6378245.0, 1/f = 298.3
        // b = a * (1 - f)
        // ee = (a^2 - b^2) / a^2;
        const double a = 6378245.0;
        const double ee = 0.00669342162296594323;


        public static Geopoint TransformFromWorldlToMars(Geopoint wgPosition)
        {
            double wgLat = wgPosition.Position.Latitude;
            double wgLon = wgPosition.Position.Longitude;
            if (outOfChina(wgLat, wgLon))
            {
                return wgPosition;
            }
            double dLat = transformLat(wgLon - 105.0, wgLat - 35.0);
            double dLon = transformLon(wgLon - 105.0, wgLat - 35.0);
            double radLat = wgLat / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);

            var position = new BasicGeoposition()
            {
                Altitude = wgPosition.Position.Altitude,
                Latitude = wgLat + dLat,
                Longitude = wgLon + dLon
            };
            return new Geopoint(position);
        }
        static bool outOfChina(double lat, double lon)
        {
            if (lon < 72.004 || lon > 137.8347)
                return true;
            if (lat < 0.8293 || lat > 55.8271)
                return true;
            return false;
        }

        static double transformLat(double x, double y)
        {
            double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y * pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        static double transformLon(double x, double y)
        {
            double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0 * pi)) * 2.0 / 3.0;
            return ret;
        }

        #endregion

        #region Mars Geodetic System <==> GCJ-02

        const double x_pi = 3.14159265358979324 * 3000.0 / 180.0;
        //将 GCJ-02 坐标转换成 BD-09 坐标
        public static Geopoint TransformFromMarsToGCJ(Geopoint marsPosition)
        {
            double x = marsPosition.Position.Longitude,
                y = marsPosition.Position.Latitude;
            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * x_pi);
            double bd_lon = z * Math.Cos(theta) + 0.0065;
            double bd_lat = z * Math.Sin(theta) + 0.006;

            var position = new BasicGeoposition()
            {
                Altitude = marsPosition.Position.Altitude,
                Latitude = bd_lon,
                Longitude = bd_lat
            };
            return new Geopoint(position);
        }

        //将 BD-09 坐标转换成  GCJ-02坐标
        public static Geopoint TransformFromGCJToMars(Geopoint bdPosition)
        {
            double x = bdPosition.Position.Longitude - 0.0065,
            y = bdPosition.Position.Latitude - 0.006;
            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_pi);
            double gg_lon = z * Math.Cos(theta);
            double gg_lat = z * Math.Sin(theta);

            var position = new BasicGeoposition()
            {
                Altitude = bdPosition.Position.Altitude,
                Latitude = gg_lat,
                Longitude = gg_lon
            };
            return new Geopoint(position);
        }
        #endregion
    }
}
