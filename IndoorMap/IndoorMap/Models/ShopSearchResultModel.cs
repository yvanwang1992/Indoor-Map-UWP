using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndoorMap.Models
{
    /*
    [
  {
    "id": "541797d6ac4711c3332d9b4a",
    "ch_name": "中国工商银行",
    "en_name": "ICBC",
    "desc": "",
    "prodId": 4,
    "logo": null,
    "floor": {
      "id": "541797d6ac4711c3332d9b3c",
      "name": "Floor1"
    },
    "coords": {
      "x": 22.13,
      "y": 15.75
    }
  }
]
    */

    public class ShopSearchResultModel
    {
        public string id { get; set; }
        public string ch_name { get; set; }
        public string en_name { get; set; }
        public string desc { get; set; }
        public string prodId { get; set; }
        public string logo { get; set; }
        public SearchFloor floor { get; set; }
        public Coords coords { get; set; } 
    }

    public class SearchFloor
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Coords
    {
        public string x { get; set; }
        public string y { get; set; }
    }
}
