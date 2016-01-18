using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace IndoorMap.Helpers
{
    public class TimerHelper
    {
        const double duration = 600;
        double oldValue, newValue,template, distance, interval;
        DispatcherTimer timer;
        public TimerHelper(double oldvalue, double newvalue)
        {
            template = oldValue;
            oldValue = oldvalue;
            newValue = newvalue;
            interval = duration / Math.Abs(oldValue - newValue);
            distance = Math.Abs(oldValue - newValue) / interval;
        }
        
        public void Start()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(interval);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            if (template > newValue)  //from big to small
            {
                if (oldValue <= newValue)
                    timer.Stop();
                oldValue -= distance;
            }
            else if (template < newValue)//from small to big 
            {
                if (oldValue >= newValue)
                    timer.Stop();
                oldValue += distance;
            }
            else
            {
                timer.Stop();
            }
        }

        public static void TimerWithAnimation(double newValue, double oldValue)
        {
            double interval = duration / Math.Abs(oldValue - newValue);
            double distance = Math.Abs(oldValue - newValue) / interval;

            double template = oldValue;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(interval);
            timer.Start();
            timer.Tick += (sender, e) => 
            {
                Debug.WriteLine(oldValue);
                if (template > newValue)  //from big to small
                {
                    if (oldValue <= newValue)
                        timer.Stop();
                    oldValue -= distance;
                }
                else if (template < newValue)//from small to big 
                {
                    if (oldValue >= newValue)
                        timer.Stop();
                    oldValue += distance;
                }
                else
                {
                    timer.Stop();
                }
            };
        }
    }
}
