using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndoorMap.Models
{
    public class JsonMallModel
    {
        public string reason { get; set; }
        public List<MallModel> result { get; set; }
        public string error_code { get; set; }
    }

    public class MallModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string addr { get; set; }
        public string desc { get; set; }
        public string opentime { get; set; }
        public string traffic { get; set; }
        public List<Building> buildings { get; set; }
        public List<string> catelogs { get; set; }
    }

    public class Building
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Floor> floors { get; set; }
    }
    
    public class Floor
    {
        public string floorB3 { get; set; }
        public string floorB2 { get; set; }
        public string floorB1 { get; set; }
        public string floor1 { get; set; }
        public string floor2 { get; set; }
        public string floor3 { get; set; }
        public string floor4 { get; set; }
        public string floor5 { get; set; }
        public string floor6 { get; set; }
        public string floor7 { get; set; }
        public string floor8 { get; set; }
        public string floor9 { get; set; }
        public string floor10 { get; set; }
        public string floor11 { get; set; }
        public string floor12 { get; set; }
    }
}
