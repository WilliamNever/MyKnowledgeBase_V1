using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace StandardLibrary.ScheduledTaskServiceTemplate.Interfaces
{
    public interface IServicesFactory
    {
        T GetRequiredService<T>(IServiceProvider ScopedProvider) where T : class;
        IEnumerable<T> GetRequiredServices<T>(IServiceProvider ScopedProvider) where T : class;
        IServiceScope RefreshProviderScope();
    }
}
