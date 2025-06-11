using Net6Test.Models;
using System.Collections.Concurrent;

namespace Net6Test.TestGroups
{
    public class ThreadTasksTest
    {
        public async static Task ContinumeWithAsync_Test()
        {
            var cans = new CancellationTokenSource();
            var tsk1 = Task.Run(async () =>
            {
                await Console.Out.WriteLineAsync("In task 1.");
                //throw new Exception("output -->>>>>>");
                Thread.Sleep(3000);
                try
                {
                    cans.Token.ThrowIfCancellationRequested();
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync($"{ex.Message} - In task 1.");
                }
                await Console.Out.WriteLineAsync($"ending - In task 1.");
            }, cans.Token);
            //var tsk2 = Task.Run(async () => { await Console.Out.WriteLineAsync("In task 2."); });
            Thread.Sleep(1500);
            cans.Cancel();
            try
            {
                var xx = await Task.WhenAll(
                    await tsk1.ContinueWith(async rsl =>
                    {
                        Console.WriteLine($"{rsl.Status} - In task 3.");
                        return new KeyValuePair<string, int>("key", 12);
                    }
                    , TaskContinuationOptions.OnlyOnRanToCompletion
                    ));
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Outter Main - {ex.Message}");
            }
            //Thread.Sleep(5000);
            await Console.Out.WriteLineAsync("Over! -> :><:");
        }

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

        public static async Task TaskAwait_Test(SimpleModel? model = null)
        {
            int i = 0;
            i = await Task.Run(async () =>
            {
                Console.WriteLine("In <-");
                await Task.Delay(5000);
                Console.WriteLine("out ->");
                if (model != null)
                    model.Name = $"Name - {model.Id}";
                return 3;
            });//.ConfigureAwait(false);
            Console.WriteLine($"Ended - {i}");
        }

        public static async Task Parallel_ForEach_Test()
        {
            var list = new List<SimpleModel>() { new SimpleModel { Id = 1 }, new SimpleModel { Id = 2 }, new SimpleModel { Id = 3 } };
            var rsl = Parallel.ForEach(list, x =>
            {
                //await TaskAwait_Test();
                //await TaskAwait_Test(x).ConfigureAwait(false);
                //TaskAwait_Test(x).ConfigureAwait(false).GetAwaiter().GetResult();
                TaskAwait_Test(x).Wait();
            });
            await Console.Out.WriteLineAsync("Run ending Point 1");
            var i = 3;
            await Task.Delay(15000);
        }
    }
}