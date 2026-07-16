using Microsoft.Extensions.DependencyInjection;

namespace StandardLibraryForDotNetX.ScheduledTaskServiceTemplate.Interfaces
{
    public interface IServicesFactory
    {
        T GetRequiredService<T>(IServiceProvider ScopedProvider) where T : class;
        IEnumerable<T> GetRequiredServices<T>(IServiceProvider ScopedProvider) where T : class;
        IServiceScope RefreshProviderScope();
    }
}
