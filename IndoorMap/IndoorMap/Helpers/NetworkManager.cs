using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace IndoorMap.Helpers
{
    public class NetworkManager
    {
        public static NetworkType ? GetNetworkInfomation()
        {
            ConnectionProfile profile = NetworkInformation.GetInternetConnectionProfile();
            if(profile == null)
            {
                return null;
            }
            //MOBILE
            if (profile.IsWwanConnectionProfile)
            {
                return NetworkType.MOBILE;
            }
            //WIFI
            if (profile.IsWlanConnectionProfile)
            {
                return NetworkType.WIFI;
            }
            return NetworkType.OTHER;
        }
    }

    public enum NetworkType
    {
        MOBILE,
        WIFI,
        OTHER
    } 
}
