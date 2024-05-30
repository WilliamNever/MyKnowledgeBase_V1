using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestGroups
{
    public class Type_Reflect_Tests
    {
        public static void IsArrayTest()
        {
            string[]? strArray = new string[] { "x" };
            List<string>? list = new List<string>() { "x" };

            var typeArray = strArray.GetType();
            var typeList = list.GetType();

            var isArray = strArray is IList nc;
            var isList = list is IList nb;

            var objArray = strArray as Array;

            //IEnumerable<string> iemb = JsonConvert.DeserializeObject<IEnumerable<string>>(JsonConvert.SerializeObject(list));
            IEnumerable<string> iemb = list.Select(x => x);//.ToList();

            var isIembList = iemb is IList;
            
            var isIembIenumerable = iemb is IEnumerable;
            var eto = iemb.GetEnumerator();
            var crnt = eto.Current;
            if (eto.MoveNext())
                crnt = eto.Current;
            var CanNxt = eto.MoveNext();

            var typeIemb = iemb.GetType();

            var isbool = iemb is IList IEList;
        }
    }
}
