using System.Threading;
using System.Threading.Tasks;

namespace StandardLibrary.ScheduledTaskServiceTemplate.Interfaces
{
    public interface IScheduledTask
    {
        /// <summary>
        /// to define whether the task can be enable
        /// </summary>
        bool IsEnabled { get; }
        Task SetupAsync(CancellationToken stoppingToken);
        Task ExecuteAsync(CancellationToken stoppingToken);
    }
}
