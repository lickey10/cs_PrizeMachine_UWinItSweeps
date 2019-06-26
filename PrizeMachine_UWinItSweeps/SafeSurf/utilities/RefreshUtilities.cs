using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCTV
{
    class RefreshUtilities_old
    {
        System.Windows.Forms.Timer goToURLTimer = new System.Windows.Forms.Timer();

        public RefreshUtilities_old()
        {
            goToURLTimer.Enabled = true;
            goToURLTimer.Tick += Timer_Tick; ;
            goToURLTimer.Interval = 1000;//one second
            goToURLTimer.Stop();
        }

        /// <summary>
        /// Stop all timers and null  the tags
        /// </summary>
        public void Cancel()
        {
            goToURLTimer.Stop();
            goToURLTimer.Tag = null;
        }

        public void GoToURL(string URL, System.Windows.Forms.TextBox txtDisplay, ExtendedWebBrowser browser)
        {
            try
            {
                if (goToURLTimer.Tag == null)
                {
                    goToURLTimer.Stop();

                    //this is how long before the link is clicked
                    Random rnd = new Random();
                    double seconds = rnd.Next(7, 15);

                    double percentage = rnd.Next(1, 100);

                    if (percentage > 80)
                        seconds = seconds + (((rnd.Next(0, 12))));
                    else if (percentage > 60)
                        seconds = seconds + ((rnd.Next(0, 10)));
                    else if (percentage > 30)
                        seconds = seconds + (((rnd.Next(0, 10))));

                    TimerInfo_old timerInfo = new TimerInfo_old();
                    timerInfo.StartTime = DateTime.Now;
                    timerInfo.UrlToGoTo = URL;
                    timerInfo.Duration = TimeSpan.FromSeconds(seconds);
                    timerInfo.TxtDisplay = txtDisplay;
                    timerInfo.Browser = browser;

                    goToURLTimer.Tag = timerInfo;
                    goToURLTimer.Tick += Timer_Tick;
                    goToURLTimer.Start();

                    //keepRunning_tour_Timer.Stop();
                    //keepRunning_tour_Timer.Tag = null;

                }
            }
            catch (Exception ex)
            {
                //Tools.WriteToFile(ex);
                throw;
                //Application.Restart();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (((System.Windows.Forms.Timer)sender).Tag is TimerInfo)
                {
                    TimerInfo timerInfo = (TimerInfo)((System.Windows.Forms.Timer)sender).Tag;
                    TimeSpan elapsedTime = DateTime.Now - timerInfo.StartTime;
                    //int elapsedMilliseconds = ((int)((double)elapsedTime.Seconds) * 1000);

                    if (elapsedTime < timerInfo.Duration)
                    {
                        if (timerInfo.TxtDisplay != null && timerInfo.TxtDisplay is System.Windows.Forms.TextBox)
                            timerInfo.TxtDisplay.Text = (timerInfo.Duration.Seconds - elapsedTime.Seconds).ToString() + " seconds";
                    }
                    else //timer is expired
                    {
                        ((System.Windows.Forms.Timer)sender).Tag = null;
                        ((System.Windows.Forms.Timer)sender).Stop();

                        if (timerInfo.TxtDisplay != null && timerInfo.TxtDisplay is System.Windows.Forms.TextBox)
                            timerInfo.TxtDisplay.Text = "0 seconds";

                        if (timerInfo.UrlToGoTo.Trim().Length > 0 && timerInfo.Browser != null && timerInfo.Browser is ExtendedWebBrowser)
                        {
                            if (!timerInfo.Browser.IsBusy)
                                timerInfo.Browser.Stop();

                            timerInfo.Browser.Url = new Uri(timerInfo.UrlToGoTo);
                        }
                    }
                }
                else
                {
                    ((System.Windows.Forms.Timer)sender).Tag = null;
                    ((System.Windows.Forms.Timer)sender).Stop();
                }
            }
            catch (Exception ex)
            {
                string found = "";
                //Tools.WriteToFile(ex);
                //throw;
                //Application.Restart();
            }
        }
    }
}
