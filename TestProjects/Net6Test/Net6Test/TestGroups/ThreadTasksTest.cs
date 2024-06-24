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

        public static async Task LockObj_Test()
        {
            object obj1 = new object();
            object obj2 = new object();

            Action act1 = () =>
                        {
                            Thread.Sleep(1000);
                            lock (obj1)
                            {
                                Console.WriteLine("111 - in");
                                lock (obj2)
                                {
                                    Console.WriteLine("222");
                                }
                                Console.WriteLine("111 - out");
                            }
                        };
            Action act2 = () =>
            {
                lock (obj2)
                {
                    Thread.Sleep(3000);
                    Console.WriteLine("Thread - in");
                    lock (obj1)
                    {
                        act1();
                        Console.WriteLine("Thread wait - delay");
                        Thread.Sleep(2000);
                    }
                }
            };
            Task.Run(act2);
            await Task.Run(act1);

            //await Task.Run(() => { act2(); act1(); });
            Console.WriteLine("Exit");
        }
    }
}
