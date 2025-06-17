using Cronos;
using TimerNotificatoin.Core.Attributes;
using TimerNotificatoin.Core.Consts;
using TimerNotificatoin.Core.Enums;

namespace TimerNotificatoin.Core.Models
{
    [HelperOutput($"class NotificationModel - alert config model")]
    public class NotificationModel
    {
        [HelperOutput("Guid Id - The identity of the alert")]
        public Guid Id { get; set; }

        #region For Template
        [HelperOutput("Guid NotificationTemplateModel id - applied NotificationTemplate")]
        public Guid? NTemplateId { get; set; }
        [HelperOutput("Date time EndDatetime - the ends date. if it is null, it indicates the notification is endless.")]
        public DateTime? EndDatetime { get; set; } = null;
        [HelperOutput("Indicated if the next alert date time used Cron express time or used the pre-set time in alert date time. " +
            "True used Cron express, False used Pre-set time.")]
        public bool? UseCronTime { get; set; }
        #endregion

        [HelperOutput("DateTime AlertDateTime - Alert Date Time")]
        public virtual DateTime AlertDateTime { get; set; }
#if !Debug
#endif
        [Newtonsoft.Json.JsonIgnore]
        public virtual DateTime CurrentAlertDateTime { get => currAlertDate ?? AlertDateTime; set => currAlertDate = value; }
        private DateTime? currAlertDate = null;
#if !Debug
        [Newtonsoft.Json.JsonIgnore]
#endif
        //[HelperOutput("DateTime StartDateTime - Start Date Time")]
        public virtual DateTime StartDateTime { get; set; }
        //[HelperOutput("double LeftSeconds - The seconds left to notifing, it was auto-calculated according AlertDateTime.")]
#if !Debug
        [Newtonsoft.Json.JsonIgnore]
#endif
        public double LeftSeconds { get; set; }
        [HelperOutput("bool ToAlert - Indicated if the notification will pop up")]
        public bool ToAlert { get; set; } = true;
        [HelperOutput("string Title - Indicated the title of the notification")]
        public string Title { get; set; }
        [HelperOutput("string Description - Indicated the Description of the notification")]
        public string Description { get; set; }

        //[HelperOutput("EnNotificationType NotificationType - Indicated if the notification can be remained after alerted")]
        //public EnNotificationType NotificationType { get; set; } = EnNotificationType.Common;
        [HelperOutput("int ClassificationID - Indicated which Classification is applied")]
        public int ClassificationID { get; set; }
#if !Debug
        [Newtonsoft.Json.JsonIgnore]
#endif
        public EnNotificationType NotificationType
        {
            get =>
                HOSTServices.GetClassifications().FirstOrDefault(x => x.ID == ClassificationID)?.NotificationType
                ?? new ClassificationModel().NotificationType;
        }
#if !Debug
        [Newtonsoft.Json.JsonIgnore]
#endif
        public string ClassificationName
        {
            get =>
                HOSTServices.GetClassifications().FirstOrDefault(x => x.ID == ClassificationID)?.Name
                ?? new ClassificationModel().Name;
        }

        public NotificationModel()
        {
            Id = Guid.NewGuid();
            AlertDateTime = DateTime.Now.Date;
        }

        public void LoopReset(DateTime dt)
        {
            if ((NotificationType & EnNotificationType.Loop) == EnNotificationType.Loop && NTemplateId.HasValue)
            {
                try
                {
                    _ = ResetNotification(this, dt);
                }
                catch (Exception ex)
                {
                }
            }
        }

        public static DateTime? GetNextRunDateTime(NotificationModel model, DateTime dt)
        {
            var tmp = HOSTServices.GetTemplates().FirstOrDefault(x => x.Id == model.NTemplateId);
            if (tmp == null) return null;

            DateTime? curNDt = null;

            CronExpression expression = CronExpression.Parse(tmp.CronoExp);
            var next = expression.GetNextOccurrence(new DateTimeOffset(dt), TimeZoneInfo.Local);
            if (next.HasValue)
            {
                if (model.UseCronTime.HasValue && model.UseCronTime.Value)
                {
                    curNDt = next.Value.DateTime;
                }
                else
                {
                    curNDt = next.Value.Date.Add(
                        new TimeSpan(model.AlertDateTime.Hour, model.AlertDateTime.Minute, model.AlertDateTime.Second));
                    if (curNDt <= dt)
                    {
                        curNDt = null;
                        var nNext = expression.GetNextOccurrence(next.Value, TimeZoneInfo.Local);
                        if (nNext.HasValue)
                        {
                            curNDt = nNext.Value.Date.Add(
                            new TimeSpan(model.AlertDateTime.Hour, model.AlertDateTime.Minute, model.AlertDateTime.Second));
                        }
                    }
                    if (!curNDt.HasValue)
                        curNDt = next.Value.DateTime;
                }
            }
            return curNDt;
        }
        private static NotificationModel ResetNotification(NotificationModel model, DateTime dt)
        {
            if (HOSTServices.GetTemplates().Any(x => x.Id == model.NTemplateId))
            {
                var curNDt = GetNextRunDateTime(model, dt);
                if (curNDt != null)
                {
                    if ((!model.EndDatetime.HasValue)
                            || (model.EndDatetime.HasValue && curNDt.Value <= model.EndDatetime.Value && model.AlertDateTime < model.EndDatetime.Value)
                            )
                    {
                        model.AlertDateTime = curNDt.Value;
                        model.LeftSeconds = model.AlertDateTime.Subtract(dt).TotalSeconds * 1000;
                        model.StartDateTime = dt;
                        model.ToAlert = true;
                    }
                }
            }
            return model;
        }
    }
}