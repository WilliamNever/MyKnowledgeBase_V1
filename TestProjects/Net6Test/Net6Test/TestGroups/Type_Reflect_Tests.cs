using Net6Test.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestGroups
{
    public class Type_Reflect_Tests
    {
        public static void RunTest()
        {
            //IsArrayTest();
            //TypeCompareTest<string>();
            ExpressionTest();
        }

        public static void ExpressionTest()
        {
            TKModel tK = new TKModel() { item = "XXXX!" };
            var exp = CreateExpression(tK, "item", tK.item);
            var vl1 = exp.Compile().Invoke(tK);
            var vl2 = exp.Compile().Invoke(new TKModel { item = "XXXX2" });
        }
        private static Expression<Func<TS, TK>> CreateExpression<TS, TK>(TS obj,string propertyName, TK value)
        {
            var parameter = Expression.Parameter(typeof(TS), "x");
            var property = typeof(TS).GetProperty(propertyName);
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            //var constant = Expression.Constant(value);
            //var equal = Expression.Assign(propertyAccess, constant);
            return Expression.Lambda<Func<TS, TK>>(propertyAccess, parameter);
        }

        public static void TypeCompareTest<T>()
        {
            var tpy = typeof(T);
            var strTpy = typeof(TKModel);
            bool isEqual = tpy.Equals(strTpy);
        }

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
