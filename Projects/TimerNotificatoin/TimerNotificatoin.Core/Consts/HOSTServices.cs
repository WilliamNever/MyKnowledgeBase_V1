using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TimerNotificatoin.Core.Interfaces;
using TimerNotificatoin.Core.Models;
using TimerNotificatoin.Core.Services;
using TimerNotificatoin.Core.Settings;

namespace TimerNotificatoin.Core.Consts
{
    public static class HOSTServices
    {
        private static IServiceProvider Provider;
        private static List<ClassificationModel> Classifications;

        public static void InitHostServices(IServiceProvider provider)
        {
            Provider = provider;
            Classifications = GetRequiredService<IOptions<List<ClassificationModel>>>().Value;
        }

        public static List<ClassificationModel> GetClassifications()
        {
            return Classifications;
        }
        public static TS? GetService<TS>()
        {
            return Provider.GetService<TS>();
        }
        public static TS GetRequiredService<TS>() where TS : notnull
        {
            return Provider.GetRequiredService<TS>();
        }
        public static IEnumerable<TS> GetServices<TS>()
        {
            return Provider.GetServices<TS>();
        }
        public static TimerServices GetTimerServices(INotificatoinMessage fm, IEnumerable<NotificationModel> notifications)
        {
            var TmSettings = Provider.GetRequiredService<IOptions<TimerSettings>>();
            return new TimerServices(TmSettings, fm, notifications ?? new List<NotificationModel>());
        }

    }
}
