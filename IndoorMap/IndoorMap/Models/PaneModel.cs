using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndoorMap.Models
{
    public class PaneModel
    {
        public string Icon { get; set; }
        public string Label { get; set; }
        public PanelItemType type { get; set; }
    }
     
    public enum PanelItemType
    {
        PanelItemMallList,
        PanelItemMap,
        PanelItemFeedback,
        PanelItemSetting,
        PanelItemReview,
        PanelItemAbout
    }
}
