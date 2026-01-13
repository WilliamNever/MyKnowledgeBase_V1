using System;
using System.Threading;
using System.Threading.Tasks;

namespace StandardLibrary.ScheduledTaskServiceTemplate.Models
{
    public class ConurrentTaskModel : IDisposable
    {
        public Task Task { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }

        public void Dispose()
        {
            try
            {
                if (!CancellationTokenSource.IsCancellationRequested)
                {
                    CancellationTokenSource.Cancel();
                }
                CancellationTokenSource.Dispose();
            }
            catch
            {
            }
        }
    }
}
