using Microsoft.VisualBasic;
using System.Runtime;
using System.Text.Encodings.Web;
using System.Timers;
using TimerNotificatoin.Core.Attributes;
using TimerNotificatoin.Core.Consts;
using TimerNotificatoin.Core.Enums;
using TimerNotificatoin.Core.Helpers;
using TimerNotificatoin.Core.Models;

namespace ConsolePreTest.Tests
{
    public static class CommonTest
    {
        public static void UTTest1()
        {
            //Console.WriteLine(Environment.CurrentDirectory);
            //Console.WriteLine(ConstsParams.TemplateFilePath);
            //Console.WriteLine(ConstsParams.AlertFilePath);
            try
            {
                var endDt = DateTime.Parse("2024-02-29");
            }
            catch (Exception ex)
            { }
            DayOfWeek dt = DateTime.Now.DayOfWeek;


            var tam = new TemplateAlertDateModel(2024, 11, 3,
                new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Wednesday }
                , EnSpecialDays.LastWeek | EnSpecialDays.FirstDay);
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(tam);
            var des = Newtonsoft.Json.JsonConvert.DeserializeObject<TemplateAlertDateModel>(str);
        }

        public static void UTTest2()
        {
            var assmb = typeof(EnMessageType).Assembly;
            var enums = assmb.DefinedTypes.Where(x => x.BaseType == typeof(Enum)).ToList();
            foreach (var en in enums)
            {
                Console.WriteLine(en.Name);
                var names = Enum.GetNames(en.AsType());
                foreach (var name in names)
                {
                    var enmVle = (int)Enum.Parse(en.AsType(), name, true);
                    Console.WriteLine($"{name} - {enmVle}");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            var configClass = assmb.DefinedTypes.Where(x =>
            x.BaseType == typeof(object)
            && (x.FullName ?? "").StartsWith("TimerNotificatoin.Core.Models")
            ).ToList();
        }


        public static T? ConvertToEnum<T>(string str, bool ignoreCases = true) where T : struct
        {
            T? resl = null;
            if (Enum.TryParse(str, ignoreCases, out T re))
            {
                resl = re;
            }
            return resl;
        }

        public static void UTTest3()
        {
            var dt = DateTime.Now;
            var tod = dt.TimeOfDay;
            var dofm = DateTime.DaysInMonth(2023, 2);
            var assmb = typeof(EnMessageType).Assembly;
            var objects = assmb.DefinedTypes.Where(x =>
            x.CustomAttributes.Any(y => y.AttributeType == typeof(HelperOutputAttribute)))
                .ToList();
            foreach (var en in objects)
            {
                var cAttr = en.GetCustomAttributes(typeof(HelperOutputAttribute), true).First() as HelperOutputAttribute;
                Console.WriteLine($"{cAttr.Description}");
                var mems = en.GetMembers().Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HelperOutputAttribute)));
                foreach (var mem in mems)
                {
                    var attrs = mem.GetCustomAttributes(typeof(HelperOutputAttribute), true).FirstOrDefault() as HelperOutputAttribute;
                    if (attrs != null)
                    {
                        Console.WriteLine($"\t{attrs.Description}");
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public static void UTStackTest()
        {
            Stack<string> stack = new Stack<string>();

            // 入栈操作
            stack.Push("First");
            stack.Push("Second");
            stack.Push("Third");

            // 查看堆栈顶元素
            if (stack.Count > 0)
            {
                Console.WriteLine("The top one: " + stack.Peek());
            }

            // 出栈操作
            while (stack.Count > 0)
            {
                Console.WriteLine("Pop element: " + stack.Pop());
            }

            Console.WriteLine("------------------------------------->");

            Queue<string> queue = new Queue<string>();

            // 入栈操作
            queue.Enqueue("First");
            queue.Enqueue("Second");
            queue.Enqueue("Third");

            // 查看堆栈顶元素
            if (queue.Count > 0)
            {
                Console.WriteLine("The top one: " + queue.Peek());
            }

            // 出栈操作
            while (queue.Count > 0)
            {
                Console.WriteLine("Dequeue element: " + queue.Dequeue());
            }

        }

        public static void UTTimerTest()
        {
            var pth = @"D:\WQPersonal\PrvCustomerTools\ConfigAlerts\Alerts.json\";//
            var fn = (Path.GetFileName(null) ?? "").ToArray();
            var path = Path.GetFullPath(pth).TrimEnd(fn);

            System.Timers.Timer MainTimer = new(5000)
            {
                AutoReset = false,
            };
            MainTimer.Elapsed += MainTimer_Elapsed;
            Console.WriteLine(DateTime.Now);
            MainTimer.Start();
            MainTimer.Stop();
            Console.WriteLine(DateTime.Now);
            Thread.Sleep(10000);
            Console.WriteLine(DateTime.Now);
            MainTimer.Start();
        }

        private static void MainTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Console.WriteLine(DateTime.Now);
        }

        public static void JSEncodingTest()
        {
            var str = ConversionsHelper.NJ_SerializeToJson("X一二", new Newtonsoft.Json.JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
            });
            Console.WriteLine(str);
        }
    }
}
