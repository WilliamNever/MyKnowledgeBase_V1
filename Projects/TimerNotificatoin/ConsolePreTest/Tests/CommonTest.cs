using TimerNotificatoin.Core.Attributes;
using TimerNotificatoin.Core.Consts;
using TimerNotificatoin.Core.Enums;
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
                var cAttr = en.GetCustomAttributes(typeof(HelperOutputAttribute),true).First() as HelperOutputAttribute;
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
    }
}
