using Cronos;
using StandardLibrary.SecurityCryptography;

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

        public static void DateTimeTimeZoneComparisonTest()
        {
            foreach (var item in expresses)
            {
                PrintNextOccurrenceComparison(item, DateTime.Now);
            }
        }

        public static DateTime? GetNextRunningDateTime(string cronoExpression, DateTime dateTime, bool inclusive = false)
        {
            CronExpression expression = CronExpression.Parse(cronoExpression, CronFormat.Standard);
            //TimeZoneInfo.Local is indicated that cronoExpression is UTC time or Local time.
            var nextRunUtc = expression.GetNextOccurrence(dateTime.ToUniversalTime(), TimeZoneInfo.Local, inclusive);
            return nextRunUtc;
        }
        public static DateTime? GetNextUTCRunningDateTime(string cronoExpression, DateTime dateTime, bool inclusive = false)
        {
            CronExpression expression = CronExpression.Parse(cronoExpression, CronFormat.Standard);
            //TimeZoneInfo.Utc is indicated that cronoExpression is UTC time or Local time.
            var nextRunUtc = expression.GetNextOccurrence(dateTime.ToUniversalTime(), TimeZoneInfo.Utc, inclusive);
            return nextRunUtc;
        }

        public static async Task CreateDTimeTest()
        {
            int round = 1;
            var expString = "0 17 * * *";
            for (var i = 0; i < round; i++) {
                var dt = DateTime.Now;
                Console.WriteLine($"{dt} - {dt.ToUniversalTime()}");
                Console.WriteLine(GetNextRunningDateTime(expString, dt));
                Console.WriteLine(GetNextUTCRunningDateTime(expString, dt));
                await Task.Delay(1000);
            }
        }
        public static async Task CreateDTimeTest_1()
        {
            var md5 = SecurityCryptography.GenerateMD5Hash("111111");

            int round = 120;
            var tStr = "2026-07-14 16:16:00";
            var expString = "* * * * *";
            for (var i = 0; i < round; i++) {
                var dt = DateTime.Parse(tStr);
                Console.WriteLine($"{dt} - {dt.ToUniversalTime()}");
                Console.WriteLine(GetNextRunningDateTime(expString, dt, true));
                Console.WriteLine(GetNextUTCRunningDateTime(expString, dt));
                Console.WriteLine();
                await Task.Delay(1000);
            }
        }

        private static void PrintNextOccurrenceComparison(string cronoExpression, DateTime dateTime)
        {
            CronExpression expression = CronExpression.Parse(cronoExpression, CronFormat.Standard);
            var startUtc = dateTime.ToUniversalTime();

            var nextLocalScheduleUtc = expression.GetNextOccurrence(startUtc, TimeZoneInfo.Local);
            var nextUtcScheduleUtc = expression.GetNextOccurrence(startUtc, TimeZoneInfo.Utc);

            Console.WriteLine($"Cron: {cronoExpression}");
            Console.WriteLine($"Start Local               : {startUtc.ToLocalTime():yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Start UTC                 : {startUtc:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Local schedule -> UTC     : {FormatUtc(nextLocalScheduleUtc)}");
            Console.WriteLine($"Local schedule -> Local   : {FormatLocal(nextLocalScheduleUtc)}");
            Console.WriteLine($"UTC schedule -> UTC       : {FormatUtc(nextUtcScheduleUtc)}");
            Console.WriteLine($"UTC schedule -> Local     : {FormatLocal(nextUtcScheduleUtc)}");
            Console.WriteLine("Meaning:");
            Console.WriteLine("- TimeZoneInfo.Local = run by local clock time.");
            Console.WriteLine("- TimeZoneInfo.Utc   = run by UTC clock time.");
            Console.WriteLine("-------------------------------------------");
        }

        private static string FormatUtc(DateTime? value)
        {
            return value.HasValue
                ? value.Value.ToString("yyyy-MM-dd HH:mm:ss")
                : "null";
        }

        private static string FormatLocal(DateTime? value)
        {
            return value.HasValue
                ? value.Value.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")
                : "null";
        }
    }
}
