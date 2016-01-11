using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndoorMap.Controller
{
    public class WaitingPanelHelper
    {
        private static WaitingPanel waitingPanel;

        public static void ShowWaitingPanel()
        {   
            if(waitingPanel == null)
            {
                waitingPanel = new WaitingPanel();
            }
            waitingPanel.Show();
        }

        public static void HiddenWaitingPanel()
        {
            if (waitingPanel != null)
                waitingPanel.Hide();
        }

        public static bool IsWaitingPanelExisted()
        {
            if (waitingPanel != null)
                return waitingPanel.isOpen;
            return false;
        }
    }
}
