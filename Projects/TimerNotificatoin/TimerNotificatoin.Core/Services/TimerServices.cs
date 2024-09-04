using TimerNotificatoin.Core.Interfaces;
using TimerNotificatoin.Core.Models;

namespace TimerNotificatoin.Core.Services
{
    public class TimerServices
    {
        protected readonly System.Timers.Timer MainTimer;
        public object SynchronizingObject = new();
        protected readonly INotificatoinMessage notificatoin;
        protected List<NotificationModel> Notifications { get; set; } = new List<NotificationModel>();
        public TimerServices(double Interval, bool AutoReset
            , INotificatoinMessage Notificatoin
            , IEnumerable<NotificationModel> notifications)
        {
            MainTimer = new(Interval)
            {
                AutoReset = AutoReset,
            };
            MainTimer.Elapsed += MainTimer_Elapsed;
            notificatoin = Notificatoin;
            Notifications.AddRange(notifications);
        }

        private void MainTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            var actNotifies = new List<NotificationModel>();
            lock (SynchronizingObject)
            {
                Notifications.ForEach(x => {
                    x.LeftSeconds -= MainTimer.Interval;
                });
                actNotifies.AddRange(Notifications.Where(x => !(x.LeftSeconds > 0) && !x.IsAlerted));
                actNotifies.ForEach(x => x.IsAlerted = true);
            }
            if (actNotifies.Any())
            {
                foreach (var noty in actNotifies) {
                    notificatoin.ShowMessage(noty, Enums.EnMessageType.NotificationShow);
                }
            }
        }
        public List<NotificationModel> GetActiveNotification() =>
             Notifications.Where(x => !x.IsAlerted).ToList();
        
        public void Start(List<NotificationModel>? notifications = null) {
            if (notifications != null)
            {
                lock (SynchronizingObject) Notifications.AddRange(notifications);
            }

            DateTime now = DateTime.Now;
            Notifications.ForEach(x => {
                x.LeftSeconds = x.AlertDateTime.Subtract(now).TotalSeconds * 1000;
            });
            MainTimer.Start();
        }
        public void Stop() => MainTimer.Stop();
    }
}
