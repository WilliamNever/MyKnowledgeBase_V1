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

            Func<int, Task> act = async (i) =>
                {
                    await Task.Run(() =>
                    {
                        bag.Add(i);
                        lock (bagList)
                        {
                            bagList.Add(i);
                        }
                        Console.WriteLine($"Enter - {i} - {Thread.CurrentThread.ManagedThreadId}");
                        Thread.Sleep(3000);
                        Console.WriteLine($"Exit - {i} - {Thread.CurrentThread.ManagedThreadId}");
                    });
                };

            //List<Task> tasks = new List<Task>();
            //for (int i = 0; i < 1000; i++)
            //{
            //    tasks.Add(act(i));
            //}
            //Task.WaitAll(tasks.ToArray());

            var numList = (new int[1000]).ToList();
            for (int m = 0; m < numList.Count; m++)
            {
                numList[m] = m;
            }
            await Parallel.ForEachAsync(numList,
                new ParallelOptions { MaxDegreeOfParallelism = 3 },
                (itm, cnlt) => new ValueTask(act(itm)));

            var list = bag.ToList();
            var blst = bagList.ToList();
        }

    }
}
