using Microsoft.Extensions.Options;
using TimerNotificatoin.Core.Interfaces;
using TimerNotificatoin.Core.Models;
using TimerNotificatoin.Core.Settings;

namespace TimerNotificatoin.Core.Services
{
    public class TimerServices : IDisposable
    {
        protected readonly System.Timers.Timer MainTimer;
        public object SynchronizingObject = new();
        protected readonly INotificatoinMessage notificatoin;
        protected readonly TimerSettings TmSettings;
        protected List<NotificationModel> Notifications { get; set; } = new List<NotificationModel>();
        public TimerServices(IOptions<TimerSettings> TimerSettings
            , INotificatoinMessage Notificatoin
            , IEnumerable<NotificationModel> notifications)
        {
            TmSettings = TimerSettings.Value;
            MainTimer = new(TmSettings.Interval)
            {
                AutoReset = TmSettings.AutoReset,
            };
            MainTimer.Elapsed += MainTimer_Elapsed;
            notificatoin = Notificatoin;
            Notifications.AddRange(notifications);
        }

        private void MainTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            bool isAllAlerted = false;
            var actNotifies = new List<NotificationModel>();
            lock (SynchronizingObject)
            {
                Notifications.ForEach(x =>
                {
                    x.LeftSeconds -= MainTimer.Interval;
                });
                actNotifies.AddRange(Notifications.Where(x => !(x.LeftSeconds > 0) && !x.IsAlerted));
                actNotifies.ForEach(x => x.IsAlerted = true);

                isAllAlerted = Notifications.All(x => x.IsAlerted);
                if (isAllAlerted) Stop();
            }

            if (isAllAlerted)
            {
                notificatoin.ShowMessage("No active alert!",
                    Enums.EnMessageType.MessageShow
                    | Enums.EnMessageType.StatusShow
                    | Enums.EnMessageType.Stopped);
            }
            else
            {
                if (!MainTimer.AutoReset)
                {
                    ResetTimer();
                    MainTimer.Start();
                }
            }


            if (actNotifies.Any())
            {
                notificatoin.ShowMessage(actNotifies, Enums.EnMessageType.NotificationShow);
            }
        }
        public List<NotificationModel> GetActiveNotification()
        {
            lock (SynchronizingObject) 
                return Notifications.Where(x => !x.IsAlerted).OrderBy(x => x.AlertDateTime).ToList();
        }
        public List<NotificationModel> GetTotalNotification()
        {
            lock (SynchronizingObject)
                return Notifications.OrderBy(x => x.IsAlerted).ThenBy(x => x.AlertDateTime).ToList();
        }

        public void Start()
        {
            Stop();
            lock (SynchronizingObject)
            {
                DateTime now = DateTime.Now;
                Notifications.ForEach(x =>
                {
                    x.LeftSeconds = x.AlertDateTime.Subtract(now).TotalSeconds * 1000;
                });
            }
            if (!MainTimer.AutoReset) ResetTimer();
            
            MainTimer.Start();
            notificatoin.ShowMessage("In progressing", Enums.EnMessageType.Started | Enums.EnMessageType.StatusShow);
        }
        private void ResetTimer()
        {
            var nnotice = GetNearestNotify();
            if (nnotice != null)
            {
                var itv = nnotice.LeftSeconds < 1 ? TmSettings.Interval : nnotice.LeftSeconds;
                itv = itv > int.MaxValue ? int.MaxValue : itv;
                MainTimer.Interval = itv;
            }
        }
        private NotificationModel? GetNearestNotify()
        {
            return Notifications.Where(x => !x.IsAlerted).OrderBy(x => x.AlertDateTime).FirstOrDefault();
        }
        public void ResetAlerts(List<NotificationModel> notifications)
        {
            Stop();
            Notifications.Clear();
            Notifications.AddRange(notifications);
        }
        public void AppendOrReplaceAlerts(List<NotificationModel> notifications)
        {
            Stop();
            Notifications.RemoveAll(x => notifications.Any(y => y.Id == x.Id));
            Notifications.AddRange(notifications);
        }
        public void Stop() { 
            MainTimer.Stop();
            notificatoin.ShowMessage("Stopped", Enums.EnMessageType.Stopped | Enums.EnMessageType.StatusShow);
        }
        public bool TimerIsRunning => MainTimer.Enabled;

        public void Dispose()
        {
            if (MainTimer.Enabled) MainTimer.Stop();

            MainTimer.Close();
            MainTimer.Dispose();
        }
    }
}
