using Microsoft.Extensions.DependencyInjection;
using StandardLibrary.ScheduledTaskServiceTemplate.Interfaces;
using System;
using System.Collections.Generic;

namespace StandardLibrary.ScheduledTaskServiceTemplate
{
    public class ServicesFactory: IServicesFactory
    {
        private object _lock = new object();
        
        private IServiceProvider _serviceProvider;
        public ServicesFactory(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }
        public T GetRequiredService<T>(IServiceProvider ScopedProvider) where T : class
        {
            return ScopedProvider.GetRequiredService<T>();
        }
        public IEnumerable<T> GetRequiredServices<T>(IServiceProvider ScopedProvider) where T : class
        {
            return ScopedProvider.GetServices<T>();
        }
        public IServiceScope RefreshProviderScope()
        {
            lock (_lock)
            {
                return _serviceProvider.CreateScope();
            }
        }
    }
}
