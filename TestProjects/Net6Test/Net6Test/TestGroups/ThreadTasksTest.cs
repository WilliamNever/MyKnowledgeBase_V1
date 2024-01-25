using Net6Test.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestGroups
{
    public class ThreadTasksTest
    {
        public async static Task ConcurrentBag_T_Test()
        {
            var bag = new ConcurrentBag<int>();
            var bagList = new List<int>();

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 1000; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    bag.Add(i);
                    lock (bagList)
                    {
                        bagList.Add(i);
                    }
                }));
            }

            var list = bag.ToList();
            var blst = bagList.ToList();
        }

    }
}
