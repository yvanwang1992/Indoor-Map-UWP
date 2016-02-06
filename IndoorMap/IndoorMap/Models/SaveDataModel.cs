using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndoorMap.Models
{
    public class SavedMallListModel
    {
        public CityModel City { get; set; }
        public List<MallModel> MallList { get; set; } 
    }
}
