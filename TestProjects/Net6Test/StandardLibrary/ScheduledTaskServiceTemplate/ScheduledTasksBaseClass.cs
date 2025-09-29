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
        public abstract string CronoExpress { get; }
        public abstract DateTime? NextRunDateTime { get; protected set; }

        protected SemaphoreSlim SSlim = new SemaphoreSlim(0);
        protected ConcurrentQueue<TKey> Sids = new ConcurrentQueue<TKey>();
        public ConcurrentDictionary<TKey, ConurrentTaskModel> TaskBags = new ConcurrentDictionary<TKey, ConurrentTaskModel>();
        protected readonly TaskSettings _taskSettings;

        protected ILogger<T> _logger;
        public ScheduledTasksBaseClass(ILogger<T> logger, TaskSettings taskSettings)
        {
            _logger = logger;
            _taskSettings = taskSettings;
        }
        public abstract Task ExecuteAsync(CancellationToken stoppingToken);
        public virtual async Task SetupAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(ReleaseResources);
            _logger.LogInformation("ScheduledTasksBaseClass.SetupAsync load data at: {time}", DateTimeOffset.Now);
            _ = DistributeWorksAsync(stoppingToken);
            await Task.CompletedTask;
        }
        public abstract void ReleaseResources();

        private async Task DistributeWorksAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DistributeNewWorksAsync(Sids, TaskBags, stoppingToken);
                    await CheckWorkingResultAsync(Sids, TaskBags, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to DistributeWorksAsync. Sids count - {Sids.Count}, TaskBags count - {TaskBags.Count}");
                }
            }
        }

        private async Task CheckWorkingResultAsync(ConcurrentQueue<TKey> sids, ConcurrentDictionary<TKey, ConurrentTaskModel> bags, CancellationToken stoppingToken)
        {
            var tss = bags.Select(x => x.Value.Task).ToList();
            if (tss.Count > 0)
            {
                var t = await Task.WhenAny(tss);
            }
            if (bags.Count < 1 && sids.Count < 1)
            {
                await NoInBoundDataAWaitAsync(stoppingToken);
            }
        }

        protected abstract Task NoInBoundDataAWaitAsync(CancellationToken stoppingToken);

        private async Task DistributeNewWorksAsync(ConcurrentQueue<TKey> sids, ConcurrentDictionary<TKey, ConurrentTaskModel> bags, CancellationToken stoppingToken)
        {
            var ccout = bags.Count;
            if (ccout < _taskSettings.WorkingTasks)
            {
                var left = _taskSettings.WorkingTasks - ccout;
                for (int i = 0; i < left; i++)
                {
                    if (sids.TryDequeue(out var sid) && bags.TryAdd(sid, new ConurrentTaskModel()))
                    {
                        var tkSource = new CancellationTokenSource();
                        bags[sid].CancellationTokenSource = tkSource;
                        bags[sid].Task = Task.Run(async () => await DealOneWorkAsync(sid, tkSource.Token), tkSource.Token);
                    }
                }
            }
            await Task.CompletedTask;
        }

        protected abstract Task DealOneWorkAsync(TKey sid, CancellationToken token);
    }

}
