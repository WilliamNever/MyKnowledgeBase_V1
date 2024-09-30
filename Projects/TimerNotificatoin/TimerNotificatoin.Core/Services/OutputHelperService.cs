using System.Text;
using TimerNotificatoin.Core.Attributes;
using TimerNotificatoin.Core.Consts;

namespace TimerNotificatoin.Core.Services
{
    public class OutputHelperService
    {
        private static object _lock = new object();
        public void WriteHelperFile()
        {
            lock(_lock)
            {
                WriteFile();
            }
        }
        public string ReadHelperFile()
        {
            if (!File.Exists(ConstsParams.HelperFilePath))
            {
                WriteHelperFile();
            }
            lock (_lock)
            {
                using StreamReader sr = new(ConstsParams.HelperFilePath, Encoding.UTF8);
                var txt = sr.ReadToEnd();
                return txt;
            }
        }
        private void WriteFile()
        {
            if(!Directory.Exists(ConstsParams.ConfigDirectory))
                Directory.CreateDirectory(ConstsParams.ConfigDirectory);

            using StreamWriter sw = new(ConstsParams.HelperFilePath,
                false, Encoding.UTF8);

            var assmb = GetType().Assembly;
            var objects = assmb.DefinedTypes.Where(x =>
            x.CustomAttributes.Any(y => y.AttributeType == typeof(HelperOutputAttribute)))
                .ToList();
            foreach (var en in objects)
            {
                HelperOutputAttribute cAttr = (HelperOutputAttribute)en.GetCustomAttributes(typeof(HelperOutputAttribute), true).First();
                sw.WriteLine($"{cAttr.Description}");

                var mems = en.GetMembers().Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HelperOutputAttribute)));
                foreach (var mem in mems)
                {
                    var attrs = mem.GetCustomAttributes(typeof(HelperOutputAttribute), true).FirstOrDefault() as HelperOutputAttribute;
                    if (attrs != null)
                    {
                        sw.WriteLine($"\t{attrs.Description}");
                    }
                }
                sw.WriteLine();
                sw.WriteLine();
            }
            sw.WriteLine();
        }
    }
}
