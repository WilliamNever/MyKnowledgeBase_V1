using Microsoft.Extensions.Logging;
using StandardLibrary.ScheduledTaskServiceTemplate.Models;
using StandardLibrary.ScheduledTaskServiceTemplate.Settings;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StandardLibrary.ScheduledTaskServiceTemplate
{
    public abstract class ScheduledTasksBaseClass<T, TKey> //where TKey : notnull
    {
        /// <summary>
        /// Sync flag
        /// </summary>
        protected object _lock = new object();
        protected CancellationToken StopCancellationToken;
        public abstract string CronoExpress { get; }
        public abstract DateTime? NextRunDateTime { get; protected set; }

        protected SemaphoreSlim SSlim = new SemaphoreSlim(0);
        protected ConcurrentQueue<TKey> Sids = new ConcurrentQueue<TKey>();
        public ConcurrentDictionary<TKey, ConurrentTaskModel> TaskBags = new ConcurrentDictionary<TKey, ConurrentTaskModel>();
        protected readonly TaskSettings _taskSettings;

        protected ILogger<T> _logger;
        protected TimeSpan? _taskTimeout;
        public ScheduledTasksBaseClass(ILogger<T> logger, TaskSettings taskSettings)
        {
            _logger = logger;
            _taskSettings = taskSettings;
        }
        public abstract Task ExecuteAsync(CancellationToken stoppingToken);
        public virtual async Task SetupAsync(CancellationToken stoppingToken)
        {
            StopCancellationToken = stoppingToken;
            stoppingToken.Register(ReleaseResources);
            _logger.LogInformation("ScheduledTasksBaseClass.SetupAsync load data at: {time}", DateTimeOffset.Now);
            _ = StartWorking(stoppingToken);
            await Task.CompletedTask;
        }
        public virtual void ReleaseResources()
        {
            var keys = TaskBags.Keys.ToArray();
            foreach (var key in keys)
            {
                if (TaskBags.TryRemove(key, out var obj))
                {
                    obj.Dispose();
                }
            }
        }

        protected virtual Task StartWorking(CancellationToken token) => DistributeWorksAsync(token);

        private async Task DistributeWorksAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DistributeNewWorksAsync(Sids, TaskBags, stoppingToken);
                    if (!await CheckWorkingResultAsync(Sids, TaskBags, stoppingToken))
                        break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to DistributeWorksAsync. Sids count - {Sids.Count}, TaskBags count - {TaskBags.Count}");
                }
            }
        }

        protected virtual async Task<bool> CheckWorkingResultAsync(ConcurrentQueue<TKey> sids, ConcurrentDictionary<TKey, ConurrentTaskModel> bags, CancellationToken stoppingToken)
        {
            var tss = bags.Select(x => x.Value.Task).ToList();
            if (tss.Any())
            {
                var t = await Task.WhenAny(tss);
            }

            if (bags.IsEmpty && sids.IsEmpty)
            {
                await NoInBoundDataAWaitAsync(stoppingToken);
            }
            return true;
        }

        protected abstract Task NoInBoundDataAWaitAsync(CancellationToken stoppingToken);

        protected virtual async Task DistributeNewWorksAsync(ConcurrentQueue<TKey> sids, ConcurrentDictionary<TKey, ConurrentTaskModel> bags, CancellationToken stoppingToken)
        {
            var ccout = bags.Count;
            if (ccout < _taskSettings.WorkingTasks)
            {
                var left = _taskSettings.WorkingTasks - ccout;
                for (int i = 0; i < left; i++)
                {
                    if (sids.TryDequeue(out var sid) && bags.TryAdd(sid, new ConurrentTaskModel()))
                    {
                        CancellationTokenSource tkSource;
                        if (_taskTimeout.HasValue) {
                            tkSource = new CancellationTokenSource(_taskTimeout.Value);
                        }
                        else
                        {
                            tkSource = new CancellationTokenSource();
                        }
                        bags[sid].CancellationTokenSource = tkSource;
                        bags[sid].Task = Task.Run(() => DealOneWorkOutLineAsync(sid, tkSource.Token), tkSource.Token);
                    }
                }
            }
            await Task.CompletedTask;
        }

        private async Task DealOneWorkOutLineAsync(TKey sid, CancellationToken token)
        {
            try
            {
                await DealOneWorkAsync(sid, token);
            }
            catch (Exception ex) {
                _logger.LogError(ex, $"Failed to processed #{sid}");
            }
            finally {
                if (TaskBags.TryRemove(sid, out var obj))
                {
                    obj.Dispose();
                }
            }
        }
        protected abstract Task DealOneWorkAsync(TKey sid, CancellationToken token);
    }
}
