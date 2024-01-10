using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestGroups
{
    public class QuartzCronExpressionTest
    {
        public static DateTime? ToUTCfromCron(DateTime currentRun_Utc, string cronExpression)
        {
            CronExpression expression = new CronExpression(cronExpression);
            expression.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZone.CurrentTimeZone?.StandardName);
            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(currentRun_Utc, expression.TimeZone);
            return expression?.GetNextValidTimeAfter(cstTime).GetValueOrDefault().UtcDateTime;
        }
    }
}
