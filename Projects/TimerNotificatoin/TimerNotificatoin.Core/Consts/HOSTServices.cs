using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TimerNotificatoin.Core.Helpers;
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
        private static string TemplatePath;
        private static List<NotificationTemplateModel>? NotificationTemplates;

        public static void InitHostServices(IServiceProvider provider)
        {
            Provider = provider;
            Classifications = GetRequiredService<IOptions<List<ClassificationModel>>>().Value
                .OrderBy(x => x.NotificationType).ThenBy(x => x.Name).ToList();
            TemplatePath = GetRequiredService<IOptions<AppSettings>>().Value.Templates;
        }

        public static List<ClassificationModel> GetClassifications()
        {
            return Classifications;
        }
        public static List<NotificationTemplateModel> LoadNotificationTemplatesFromFile()
        {
            var js = File.ReadAllText(TemplatePath);
            NotificationTemplates = ConversionsHelper.NJ_DeserializeToJson<List<NotificationTemplateModel>>(js);
            return NotificationTemplates;
        }
        public static List<NotificationTemplateModel> GetTemplates()
        {
            return NotificationTemplates ?? LoadNotificationTemplatesFromFile();
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
