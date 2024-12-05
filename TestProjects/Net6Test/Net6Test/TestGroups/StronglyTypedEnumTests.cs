using AutoMapper.Internal;
using Net6Test.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestGroups
{
    public class StronglyTypedEnumTests
    {
        public static void Test1()
        {
            var ewe = new Enumeration<EnOp>();
        }
    }

    public class Enumeration<T> where T : struct, Enum
    {
        private readonly List<TEnumKeyPaire> _enPairs = new();
        public Enumeration()
        {
            var nms = Enum.GetNames<T>();
            foreach (var nm in nms)
            { 
                var enVal = Enum.Parse<T>(nm);
                var intVal = (int)Convert.ChangeType(enVal, typeof(int));
                _enPairs.Add(new TEnumKeyPaire(intVal, nm, enVal));
            }
        }

        public T? GetEnumFromName(string name, bool IgnoreCases = false)
        {
            return _enPairs.FirstOrDefault(x =>
                x.name.Equals(name,
                IgnoreCases ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))?.emu;
        }
        public T? GetEnumFromIntValue(int val)
        {
            return _enPairs.FirstOrDefault(x => x.val == val)?.emu;
        }

        private record TEnumKeyPaire(int val, string name, T emu);
    }
}
