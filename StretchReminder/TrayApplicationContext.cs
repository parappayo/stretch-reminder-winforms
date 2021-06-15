using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace StretchReminder
{
    public class TrayApplicationContext : ApplicationContext
    {
        private readonly NotifyIcon _trayIcon;

        public TrayApplicationContext()
        {
            _trayIcon = new NotifyIcon()
            {
                Icon = SystemIcons.Information,
                Text = ConfigurationManager.AppSettings["notificationText"],
                BalloonTipText = ConfigurationManager.AppSettings["notificationBalloonTipText"],
                Visible = true
            };

            var timerPeriod = int.Parse(ConfigurationManager.AppSettings["notificationPeriod"]) * 1000;
            timerPeriod = Math.Min(timerPeriod, 20000);

            var timer = new System.Timers.Timer(timerPeriod);
            timer.Elapsed += ShowNotification;
            timer.Enabled = true;
        }

        private void ShowNotification(object sender, EventArgs eventArgs)
        {
            var timerDuration = int.Parse(ConfigurationManager.AppSettings["notificationDuration"]) * 1000;
            _trayIcon.ShowBalloonTip(timerDuration);
        }
    }
}