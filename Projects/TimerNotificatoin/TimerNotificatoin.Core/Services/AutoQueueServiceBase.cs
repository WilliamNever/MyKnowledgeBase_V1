﻿using Microsoft.Extensions.Options;
using System.Timers;
using TimerNotificatoin.Core.Settings;

namespace TimerNotificatoin.Core.Services
{
    public abstract class AutoQueueServiceBase<T> : IDisposable
    {
        protected readonly System.Timers.Timer MainTimer;
        public object SynchronizingObject = new();
        protected readonly AtuoSaveSettings _settings;
        protected Queue<T> _queue;

        public AutoQueueServiceBase(IOptions<AtuoSaveSettings> settings)
        {
            _queue = new Queue<T>();
            _settings = settings.Value;
            MainTimer = new(_settings.Interval)
            {
                AutoReset = _settings.AutoReset,
            };
            MainTimer.Elapsed += MainTimer_Elapsed;
        }

        protected abstract void MainTimer_Elapsed(object? sender, ElapsedEventArgs e);

        public virtual void Add(T item)
        {
            lock (SynchronizingObject)
            {
                MainTimer.Stop();
                _queue.Enqueue(item);
                MainTimer.Start();
            }
        }
        public virtual void Stop(bool clearStack = false)
        {
            MainTimer.Stop();
            if (clearStack)
            {
                lock (SynchronizingObject)
                {
                    _queue.Clear();
                }
            }
        }

        public void Dispose()
        {
            Stop(true);
            MainTimer.Close();
            MainTimer.Dispose();
        }
    }
}