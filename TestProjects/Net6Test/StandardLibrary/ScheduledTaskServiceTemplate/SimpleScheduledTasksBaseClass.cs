using Microsoft.Extensions.Logging;
using StandardLibrary.ScheduledTaskServiceTemplate.Settings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StandardLibrary.ScheduledTaskServiceTemplate
{
    public abstract class SimpleScheduledTasksBaseClass<T> : IDisposable
    {
        /// <summary>
        /// Sync flag
        /// </summary>
        protected readonly object _lock = new object();
        private CancellationTokenRegistration _stpRegistration;
        protected CancellationToken StopCancellationToken;
        public abstract string CronoExpress { get; }
        public abstract DateTime? NextRunDateTime { get; protected set; }

        protected readonly TaskSettings _taskSettings;

        protected readonly ILogger<T> _logger;
        protected SimpleScheduledTasksBaseClass(ILogger<T> logger, TaskSettings taskSettings)
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
            _logger.LogInformation("SetupAsync load data at: {time}", DateTimeOffset.Now);

            StopCancellationToken = stoppingToken;
            _stpRegistration.Dispose();
            _stpRegistration = stoppingToken.Register(ReleaseResources);

            await Task.CompletedTask;
        }
        public abstract void ReleaseResources();

        public virtual void Dispose()
        {
            _stpRegistration.Dispose();
        }
    }
}
