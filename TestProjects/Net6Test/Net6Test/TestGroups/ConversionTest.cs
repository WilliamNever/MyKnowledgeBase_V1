using Net6Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestGroups
{
    public class ConversionTest
    {
        public async static Task TypeTest()
        {
            var bss = new BaseOXSep1 { BaseOXSep1_Name = "XXXXX" };
            var valueClass = new ValueClassTmp<BaseOXSep1>() { TValue = bss };
            var dc = new Dictionary<string, IVct<object>>();
            dc.Add("a1", valueClass);

            var ivct = dc["a1"] as IVct<BaseOXSep1>;
            BaseOXSep1? oj = ivct?.GetValue();

            //var ivct = dc["a1"] as IVct<Base0>;
            //var oj = ivct?.GetValue();

            var type = bss.GetType();
            object obj = bss;
            var dest = Convert.ChangeType(obj, type);
        }
    }

    public class ValueClassTmp<T> : IVct<T>
    {
        public T TValue { get; set; }

        public T GetValue()
        {
            return TValue;
        }
    }

    public interface IVct<out T>
    {
        //T Value { get; set; }
        T GetValue();
    }
}
