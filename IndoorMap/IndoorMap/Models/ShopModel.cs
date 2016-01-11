using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndoorMap.Models
{
    public class ShopModel
    {
        public string id { get; set; }
        public string ch_name { get; set; }
        public string en_name { get; set; }
        public string logo { get; set; }
        public string catelogs { get; set; }
        public float lon { get; set; }
        public float lat { get; set; }
    }
}
