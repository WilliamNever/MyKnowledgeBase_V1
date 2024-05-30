using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestGroups
{
    public class MemoryCache_Tests
    {
        public static void Test()
        {
            MemoryCache mc = new MemoryCache(new MemoryCacheOptions() { });
            mc.Remove("xxx");
            mc.Remove(null);
        }
    }
}
