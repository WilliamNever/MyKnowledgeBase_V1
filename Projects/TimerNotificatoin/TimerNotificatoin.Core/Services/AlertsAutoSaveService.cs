﻿using Microsoft.Extensions.Options;
using System.Text;
using System.Timers;
using TimerNotificatoin.Core.Settings;

namespace TimerNotificatoin.Core.Services
{
    public class AlertsAutoSaveService : AutoStackServiceBase<string>
    {
        protected readonly AppSettings _appSettings;
        public AlertsAutoSaveService(IOptions<AtuoSaveSettings> settings, IOptions<AppSettings> appsettings) : base(settings)
        {
            _appSettings = appsettings.Value;
        }

        public override void Add(string item)
        {
            lock (SynchronizingObject)
            {
                MainTimer.Stop();
                _stack.Clear();
                _stack.Push(item);
                MainTimer.Start();
            }
        }
        protected override void MainTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            lock (SynchronizingObject)
            {
                if (_stack.Count > 0)
                {
                    var txt = _stack.Peek();
                    File.WriteAllText(_appSettings.Notifications, txt, Encoding.UTF8);
                    _stack.Clear();
                }
            }
        }
    }
}
