using Microsoft.IdentityModel.Tokens;
using Net6Test.Models;
using System.Text.Encodings.Web;
using System.Web;

namespace Net6Test.TestGroups
{
    public class ListTests
    {
        public async static Task ListForEachTest()
        {
            var enc = HttpUtility.UrlEncode("a/a\\<>& =?(_)[]{}");

            string?[] strings = new string?[] { "<>& =?(_)[]{}", null, "a/a", "a\\a" };

            var nn = strings.AsEnumerable().Select(x => HttpUtility.UrlEncode(x)).ToList();
            var m1 = strings.AsEnumerable().Select(x => x == null ? x : UrlEncoder.Default.Encode(x)).ToList();
            var m2 = strings.AsEnumerable().Select(x => x == null ? x : Base64UrlEncoder.Encode(x)).ToList();
            var m2_1 = m2.Select(x => x == null ? x : Base64UrlEncoder.Decode(x)).ToList();

            var pms = new string?[] { "", null, "aaa" }.Select(x => x);
            var rr = string.Format("-{0},{1}-{2}-", pms.ToArray());
        }

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
