using Microsoft.Extensions.Logging;
using StandardLibraryForDotNetX.ScheduledTaskServiceTemplate.Models;
using StandardLibraryForDotNetX.ScheduledTaskServiceTemplate.Settings;
using System.Collections.Concurrent;

namespace StandardLibraryForDotNetX.ScheduledTaskServiceTemplate
{
    public abstract class ScheduledTasksBaseClass<T, TKey> : IDisposable where TKey : notnull
    {
        /// <summary>
        /// Sync flag
        /// </summary>
        protected readonly object _lock = new object();
        private CancellationTokenRegistration _stpRegistration;
        protected CancellationToken StopCancellationToken;
        public abstract string CronoExpress { get; }
        public abstract DateTime? NextRunDateTime { get; protected set; }

        protected readonly SemaphoreSlim SSlim = new SemaphoreSlim(0);
        protected readonly ConcurrentQueue<TKey> Sids = new ConcurrentQueue<TKey>();
        public readonly ConcurrentDictionary<TKey, ConurrentTaskModel> TaskBags = new ConcurrentDictionary<TKey, ConurrentTaskModel>();
        protected readonly TaskSettings _taskSettings;

        protected readonly ILogger<T> _logger;
        protected TimeSpan? _taskTimeout;
        protected ScheduledTasksBaseClass(ILogger<T> logger, TaskSettings taskSettings)
        {
            _logger = logger;
            _taskSettings = taskSettings;
        }
        public abstract Task ExecuteAsync(CancellationToken stoppingToken);
        /// <summary>
        /// stoppingToken is same as the token
        /// in public abstract Task ExecuteAsync(CancellationToken stoppingToken);
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public virtual async Task SetupAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ScheduledTasksBaseClass.SetupAsync load data at: {time}", DateTimeOffset.Now);

            StopCancellationToken = stoppingToken;
            _stpRegistration = stoppingToken.Register(ReleaseResources);
            _ = StartWorking(stoppingToken);
        }
        public virtual void ReleaseResources()
        {
            Sids.Clear();
            var keys = TaskBags.Keys.ToArray();
            foreach (var key in keys)
            {
                if (TaskBags.TryRemove(key, out var obj))
                {
                    obj.Dispose();
                }
            }
            _stpRegistration.Dispose();
        }
        public virtual void Dispose()
        {
            _stpRegistration.Dispose();
        }
        protected virtual Task StartWorking(CancellationToken token) => DistributeWorksAsync(token);

        private async Task DistributeWorksAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DistributeNewWorksAsync(Sids, TaskBags, stoppingToken).ConfigureAwait(false);
                    if (!await CheckWorkingResultAsync(Sids, TaskBags, stoppingToken).ConfigureAwait(false))
                        break;
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    _logger.LogError($"Exited by Operation Canceled. Sids count - {Sids.Count}, TaskBags count - {TaskBags.Count}");
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
            if ((sids.IsEmpty && tss.Any()) || tss.Count >= _taskSettings.WorkingTasks)
            {
                _ = await Task.WhenAny(tss);
            }

            if (bags.IsEmpty && sids.IsEmpty)
            {
                await NoInBoundDataAWaitAsync(stoppingToken).ConfigureAwait(false);
            }
            return true;
        }

        protected virtual async Task NoInBoundDataAWaitAsync(CancellationToken stoppingToken)
        {
            _logger.LogTrace("Enter NoInBoundDataAWaitAsync at: {time}", DateTimeOffset.Now);
            try
            {
                await ToResetSidsAsync(stoppingToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            if (Sids.IsEmpty)
            {
                await SSlim.WaitAsync(stoppingToken);
            }
            else
            {
                await SSlim.WaitAsync(5 * 1000, stoppingToken);
            }
            _logger.LogTrace("Exit NoInBoundDataAWaitAsync at: {time}", DateTimeOffset.Now);
        }

        protected abstract Task ToResetSidsAsync(CancellationToken stoppingToken);

        protected virtual async Task DistributeNewWorksAsync(ConcurrentQueue<TKey> sids, ConcurrentDictionary<TKey, ConurrentTaskModel> bags, CancellationToken stoppingToken)
        {
            var ccout = bags.Count;
            if (ccout < _taskSettings.WorkingTasks)
            {
                var left = _taskSettings.WorkingTasks - ccout;
                for (int i = 0; i < left; i++)
                {
                    /*
                     * There is no race issue to add ConurrentTaskModel with a null task.
                     * DistributeNewWorksAsync and CheckWorkingResultAsync are in main control thread/task.
                     * They can only run step by step at a time.
                     */
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

                        // According to https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.run?view=net-6.0,
                        // Task.Run will be good for ThreadPool. Thread pool is in use here.
                        bags[sid].Task = Task.Run(() => DealOneWorkOutLineAsync(sid, tkSource.Token), tkSource.Token);

                        // the following methods, which init tasks in thread pool, will be the options for Task.Run.
                        // when selecting, please carefully examine the differences.
                        // the first one is almost equivalent to Task.Run.
                        //bags[sid].Task = Task.Factory.StartNew(() => DealOneWorkOutLineAsync(sid, tkSource.Token)
                        //, tkSource.Token, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
                        //bags[sid].Task = Task.Factory.StartNew(() => DealOneWorkOutLineAsync(sid, tkSource.Token), tkSource.Token);
                        // the following one makes the thread pool more inclined to create independent threads
                        // to avoid dragging down the thread pool. 
                        //bags[sid].Task = Task.Factory.StartNew(() => DealOneWorkOutLineAsync(sid, tkSource.Token)
                        //, tkSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                    }
                }
            }
            await Task.CompletedTask;
        }

        private async Task DealOneWorkOutLineAsync(TKey sid, CancellationToken token)
        {
            try
            {
                await DealOneWorkAsync(sid, token).ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested)
            {
                _logger.LogError($"Exited manually, break to processed #{sid}");
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
