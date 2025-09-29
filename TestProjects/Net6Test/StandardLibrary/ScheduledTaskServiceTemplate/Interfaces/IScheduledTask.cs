using System.Threading;
using System.Threading.Tasks;

namespace StandardLibrary.ScheduledTaskServiceTemplate.Interfaces
{
    public interface IScheduledTask
    {
        Task SetupAsync(CancellationToken stoppingToken);
        Task ExecuteAsync(CancellationToken stoppingToken);
    }
}
