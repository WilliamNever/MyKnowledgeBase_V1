using Microsoft.Extensions.Caching.Memory;

namespace Net6Test.TestGroups
{
    public class MemoryCache_Tests
    {
        public static void Test()
        {
            MemoryCache mc = new MemoryCache(new MemoryCacheOptions() { });
            mc.Set("xxx", "xxx");
            mc.Set("yyy", "xxx");
            mc.Remove("xxx");
            //mc.Remove(null);  // throw exception

            var rsl_xxx = mc.TryGetValue("xxx", out var rsl1);
            var rsl_yyy = mc.TryGetValue("yyy", out var rsl2);
            var rsl_zzz = mc.TryGetValue("zzz", out var rsl3);
        }
    }
}
