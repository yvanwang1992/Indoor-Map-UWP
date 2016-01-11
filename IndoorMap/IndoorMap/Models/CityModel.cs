using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndoorMap.Models
{
    public class JsonCityModel
    {
        public string reason { get; set; }
        public List<CityModel> result { get; set; }
        public string error_code { get; set; }
    }

    public class CityModel
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
