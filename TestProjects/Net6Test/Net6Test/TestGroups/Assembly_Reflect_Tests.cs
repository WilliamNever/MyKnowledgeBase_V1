using Microsoft.Extensions.DependencyInjection;
using Net6Test.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestGroups
{
    public class Assembly_Reflect_Tests
    {
        public async Task ReflectFindServicesTest()
        {
            var tinew = typeof(INew<>);

            var assembly = GetType().Assembly;
            var Iface = assembly.DefinedTypes.Where(t => t.IsInterface && t.AsType() == tinew).Select(x => x.AsType()).First();
            var objs = assembly.DefinedTypes.Where(x => !x.IsAbstract && !x.IsInterface).ToList();

            var list = objs.Where(x => x.ImplementedInterfaces.Any(y => TMakeGenericType(tinew, y))).ToList();


            await ReflectFindServicesTest_Part1();
        }
        private static bool TMakeGenericType(Type Iface, Type obj)
        {
            try
            {
                var tp = Iface.MakeGenericType(obj.GenericTypeArguments);
                return tp == obj;
            }
            catch
            {
                return false;
            }
        }
        private static bool CanBeCastTo(Type pluggedType, Type pluginType)
        {
            //if (pluggedType == null) return false;

            //if (pluggedType == pluginType) return true;

            return pluginType.GetTypeInfo().IsAssignableFrom(pluggedType.GetTypeInfo());
        }
        public async Task ReflectFindServicesTest_Part1()
        {
            var assembly = GetType().Assembly;
            var refAssemblies = assembly.GetReferencedAssemblies();
            var ass = Assembly.Load(refAssemblies[18]);
            var sass = AppDomain.CurrentDomain.GetAssemblies();

            var Iface = assembly.DefinedTypes.Where(t => t.IsInterface).Select(x => x.AsType()).FirstOrDefault(x => x.Name.StartsWith("IDotTests"));
            var objs = assembly.DefinedTypes.Where(x => !x.IsAbstract && !x.IsInterface).ToList();
            var ics = objs.Where(x => x.ImplementedInterfaces.Any(ifc => ifc == Iface)).First();

            IServiceCollection isvrs = new ServiceCollection();
            isvrs.AddTransient(Iface, ics);

            var pvdr = isvrs.BuildServiceProvider();
            var svr = pvdr.GetService<IDotTests>();
            var svr1 = pvdr.GetService<IDotTests>();

            var aa = svr.GetId("");
            var bb = svr.GetIdx(321);
        }

        public async Task ReadRunningProgress()
        {
            var processId = Process.GetCurrentProcess().Id;
            var enProcessId = Environment.ProcessId;
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            //var procs = Process.GetProcesses().Where(x =>
            //    x.ProcessName?.Equals(assemblyName, StringComparison.OrdinalIgnoreCase) ?? false);

            var proc = Process.GetProcesses().FirstOrDefault(x =>
                x.ProcessName?.Equals("notepad", StringComparison.OrdinalIgnoreCase) ?? false);
            proc?.Kill();
            //proc?.WaitForExit();
            proc?.Close();
            Console.WriteLine("run nextly ...");

            try
            {
                await Task.Run(() => { throw new Exception("T_T"); });
            }
            catch (Exception ex)
            {
            }
        }
    }
}
