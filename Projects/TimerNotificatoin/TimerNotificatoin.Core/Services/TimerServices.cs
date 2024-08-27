using TimerNotificatoin.Core.Interfaces;
using TimerNotificatoin.Core.Models;

namespace TimerNotificatoin.Core.Services
{
    public class TimerServices
    {
        protected readonly System.Timers.Timer MainTimer;
        public static object SynchronizingObject = new();
        protected readonly INotificatoinMessage notificatoin;
        protected List<NotificationModel> Notifications { get; set; } = new List<NotificationModel>();
        public TimerServices(double Interval, bool AutoReset, INotificatoinMessage Notificatoin)
        {
            MainTimer = new(Interval)
            {
                AutoReset = AutoReset,
            };
            MainTimer.Elapsed += MainTimer_Elapsed;
            notificatoin = Notificatoin;
        }

        private void MainTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            lock (SynchronizingObject)
            {
            }
        }
        public void Start(List<NotificationModel>? notifications = null) {
            if (notifications != null)
            {
                lock (SynchronizingObject) Notifications = notifications;
            }
            MainTimer.Start();
        }
        public void Stop() => MainTimer.Stop();
    }
}
