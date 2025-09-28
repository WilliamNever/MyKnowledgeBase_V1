
using Cronos;

namespace Net6Test.TestGroups
{
    public class Cronos_Tests
    {
        public static string[] expresses = new string[] { 
            "5 * * * *", 
            "0 0 * * *", 
            "0 * * * *", 
            "0 17 * * *"
        };
        public static void DateTimeTest()
        {
            foreach (var item in expresses)
            {
                Console.WriteLine(item);
                var expString = item;
                var dt = GetNextRunningDateTime(expString, DateTime.Now);
                Console.WriteLine(dt);
                dt = GetNextRunningDateTime(expString, dt.Value);
                Console.WriteLine(dt);
                dt = GetNextRunningDateTime(expString, dt.Value);
                Console.WriteLine(dt);
                Console.WriteLine("-------------------------------------------");
            }
        }

        public static DateTime? GetNextRunningDateTime(string cronoExpression, DateTime dateTime)
        {
            CronExpression expression = CronExpression.Parse(cronoExpression, CronFormat.Standard);
            var NextRunUTC = expression.GetNextOccurrence(dateTime.ToUniversalTime(), TimeZoneInfo.Local);
            return NextRunUTC?.ToLocalTime();
        }
    }
}
