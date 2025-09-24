using Net6Test.Models;

namespace Net6Test.TestGroups
{
    public class ListTests
    {
        public static void ListJoinTest()
        {
            var list = new List<Base0>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new Base0() { Base0_Name = $"aaa_{i}" });
            }
            var list2 = new List<Base0>();
            for (int i = 0; i < 10; i++)
            {
                list2.Add(new Base0() { Base0_Name = $"AAA_{i}" });
            }
            var rsls = (from x in list
                        join y in list2 on x.Base0_Name.ToLower() equals y.Base0_Name.ToLower() into dj
                        from sub in dj.DefaultIfEmpty()
                        select new { XClass = x, YClass = sub }).ToList();

        }
    }
}
