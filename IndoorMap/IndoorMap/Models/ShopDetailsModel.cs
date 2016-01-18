using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndoorMap.Models
{
    public class JsonShopDetailsModel
    {
        public string reason { get; set; }
        public ShopDetailsModel result{ get; set; }
        public string error_code{ get; set; }
    }

    public class ShopDetailsModel
    {
        public string id{ get; set; }
        public string ch_name{ get; set; }
        public string en_name{ get; set; }
        public string logo{ get; set; }
        public string catelogs{ get; set; }
        public string lon{ get; set; }
        public string lat{ get; set; }
        public List<Comment> comments{ get; set; }
        public List<Coupon> coupons{ get; set; }
        public Mall mall{ get; set; }
        public Mall building{ get; set; }
        public Mall floor{ get; set; }
    }

    public class Comment
    {
        public string user_nickname{ get; set; }
        public string create_time{ get; set; }
        public string text_excerpt{ get; set; }
        public string type{ get; set; }
    }
    
    public class Coupon
    {
        public string novalue{ get; set; }
    } 
    public class Mall //include mall、building、floor
    {
        public string id{ get; set; }
        public string name{ get; set; }
    }
}
