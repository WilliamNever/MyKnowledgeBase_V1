using Net6Test.Models;
using System.Collections.Concurrent;

namespace Net6Test.TestGroups
{
    public class ThreadTasksTest
    {
        public async static Task Task_WhenAny_With_SemaphoreSlim_Test()
        {
            CancellationTokenSource ts = new CancellationTokenSource();
            //SemaphoreSlim ss = new SemaphoreSlim(1,1);
            SemaphoreSlim ss = new SemaphoreSlim(0);
            //await ss.WaitAsync(3 * 1000, ts.Token);
            ss.Release(3);
            await ss.WaitAsync(5 * 1000, ts.Token);
            await ss.WaitAsync(5 * 1000, ts.Token);
            var needsToAdding = ss.CurrentCount > 2;
            //await ss.WaitAsync(300 * 1000, ts.Token);
            _ = Task.Run(async () =>
            {
                await Task.Delay(5000);
                try
                {
                    //ts.Cancel();
                }
                catch (Exception ex)
                {
                }
            });
            try
            {
                await ss.WaitAsync(300 * 1000, ts.Token);
            }
            catch (Exception ex)
            {
            }

            int totalTasks = 3;
            ConcurrentDictionary<string, Task<string>> bags = new();
            ConcurrentQueue<string> Sids = new();

            Func<string, string, int, Guid, Task<string>> func = async (id, name, ltime, guid) =>
            {
                var stime = ltime + 3;
                for (int i = 0; i < stime; i++)
                {
                    Console.WriteLine($"Task {name} - {DateTime.Now}");
                    await Task.Delay(1000);
                }
                bags.TryRemove(id, out _);
                Console.WriteLine($"{name} - {guid} / {ltime} // Thread ID - {Thread.CurrentThread.ManagedThreadId}");
                return $"{name} / {ltime}";
            };

            for (int i = 0; i < 10; i++)
            {
                Sids.Enqueue($"Queue - {i}");
            }

            _ = Task.Run(async () =>
            {
                await Task.Delay(60000);
                for (int i = 0; i < 10; i++)
                {
                    Sids.Enqueue($"Append Queue - {i}");
                    await Task.Delay(1000);
                    if (ss.CurrentCount < 2)
                        ss.Release(2);
                }
            });
            var gid = Guid.NewGuid();
            var t1 = Task.Run(async () => await func("1", "Thr - 1", 3, gid), ts.Token);
            var t2 = Task.Run(async () => await func("2", "Thr - 2", 3, gid), ts.Token);
            var t3 = Task.Run(async () => await func("3", "Thr - 3", 3, gid), ts.Token);
            //bags.TryAdd("1", t1);
            //bags.TryAdd("2", t2);
            //bags.TryAdd("3", t3);
            _ = bags.AddOrUpdate("1", t1, (key, tsk1) => { return t1; });
            _ = bags.AddOrUpdate("2", t2, (key, tsk1) => { return t2; });
            _ = bags.AddOrUpdate("3", t3, (key, tsk1) => { return t3; });

            await Task.Delay(5000);
            while (true)
            {
                gid = Guid.NewGuid();
                Console.WriteLine();
                Console.WriteLine($"{gid} Starting......");
                var ccout = bags.Count;
                if (ccout < totalTasks)
                {
                    var left = totalTasks - ccout;
                    for (int i = 0; i < left; i++)
                    {
                        if (Sids.TryDequeue(out var sid))
                        {
                            var m = i;
                            bags.TryAdd(sid, Task.Run(async () => await func(sid, $"Thr - {sid}", m, gid)));
                            await Task.Delay(500, ts.Token);
                        }
                    }
                }


                try
                {
                    //await Task.Delay(5000, ts.Token);
                    var tss = bags.Select(x => x.Value).ToList();
                    var t = await Task.WhenAny(tss.Count > 0 ? tss : new Task[] { Task.CompletedTask });
                    //Console.WriteLine(await t);
                }
                catch (Exception ex)
                {
                }
                if (bags.Count < 1 && Sids.Count < 1)
                {
                    var stpId = Guid.NewGuid();
                    Console.WriteLine($"Begin to sleep at {DateTime.Now} - {stpId}");
                    await ss.WaitAsync(5 * 60 * 1000, ts.Token);
                    Console.WriteLine($"Awake sleep at {DateTime.Now} - {stpId}");

                    //await ss.WaitAsync(5 * 1000, ts.Token);
                    //break;

                }
            }
        }

        public async static Task Task_WhenAny_Test()
        {
            CancellationTokenSource ts = new CancellationTokenSource();
            var sl = new ManualResetEventSlim(false);

            _ = Task.Run(async () => { 
                await Task.Delay(5000); 
                try
                {
                    //Console.WriteLine($"To cancel token");
                    sl.Set();
                    //ts.Cancel();
                }
                catch(Exception ex)
                {
                }
            });

            try
            {
                sl.Wait(ts.Token);
            }
            catch (Exception ex)
            {
            }
            finally {
                sl.Reset();
            }

            int totalTasks = 3;
            ConcurrentDictionary<string, Task<string>> bags = new();
            ConcurrentQueue<string> Sids = new();

            Func<string, string, int, Task<string>> func = async (id, name, ltime) =>
            {
                for (int i = 0; i < ltime; i++)
                {
                    Console.WriteLine($"Task {name} - {DateTime.Now}");
                    await Task.Delay(1000);
                }
                bags.TryRemove(id, out _);
                return $"{name} / {ltime}";
            };

            for (int i = 0; i < 10; i++)
            {
                Sids.Enqueue($"Queue - {i}");
            }

            _ = Task.Run(async () => {
                await Task.Delay(10000);
                for (int i = 0; i < 10; i++)
                {
                    Sids.Enqueue($"Append Queue - {i}");
                    await Task.Delay(1000);
                }
            });


            var t1 = Task.Run(async () => await func("1", "Thr - 1", 3));
            var t2 = Task.Run(async () => await func("2", "Thr - 2", 3));
            var t3 = Task.Run(async () => await func("3", "Thr - 3", 3));
            //bags.TryAdd("1", t1);
            //bags.TryAdd("2", t2);
            //bags.TryAdd("3", t3);
            _ = bags.AddOrUpdate("1", t1, (key, tsk1) => { return t1; });
            _ = bags.AddOrUpdate("2", t2, (key, tsk1) => { return t2; });
            _ = bags.AddOrUpdate("3", t3, (key, tsk1) => { return t3; });

            await Task.Delay(5000);
            //ts.Cancel();
            sl.Set();
            while (true)
            {
                sl.Wait(ts.Token);
                Console.WriteLine($"Starting......");
                
                var ccout = bags.Count;
                if (ccout < totalTasks)
                {
                    var left = totalTasks - ccout;
                    for (int i = 0; i < left; i++)
                    {
                        if (Sids.TryDequeue(out var sid))
                        {
                            bags.TryAdd(sid, Task.Run(async () => await func(sid, $"Thr - {sid}", i + 3)));
                        }
                        else
                        { }
                    }
                }

                if (bags.Count > 0)
                {
                    var t = await Task.WhenAny(bags.Select(x => x.Value));
                    Console.WriteLine(await t);
                }
                else
                {
                    if (Sids.Count < 1)
                    {
                        sl.Reset();
                        //sl.Wait(ts.Token);
                        break;
                    }
                    else
                    { }
                }
            }
        }
        public async static Task Task_Dispose_Test()
        {
            Func<CancellationToken, Task<int>> func = async (token) =>
            {
                //while (true)
                while (!token.IsCancellationRequested)
                {
                    //token.ThrowIfCancellationRequested();
                    Console.WriteLine($"In Task - {DateTime.Now}");
                    await Task.Delay(1000);
                }
                await Task.Delay(3000);
                return 3;
            };
            try
            {
                CancellationTokenSource ts = new CancellationTokenSource();
                using (var tsk = func(ts.Token))
                {
                    await Task.Delay(5000);
                    ts.Cancel();
                    //ts.Dispose(); //Dispose cannot stop the running Task.
                    var r = await tsk;
                }
            }
            catch (OperationCanceledException ex)
            {
            }
            catch (Exception ex)
            {
            }
            finally
            {
                await Task.Delay(10000);
            }
        }

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
            //var bag = new ConcurrentBag<int>();
            var bag = new BlockingCollection<int>();
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
                        //Thread.Sleep(3000);
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
                new ParallelOptions { MaxDegreeOfParallelism = 4 },
                (itm, cnlt) => new ValueTask(act(itm)));

            var list = bag.ToList();
            var blst = bagList.ToList();

            var tokenSource = new CancellationTokenSource();
            try
            {
                AddItems(bag, tokenSource);
                foreach (var itm in bag.GetConsumingEnumerable(tokenSource.Token))
                {
                    //tokenSource.Token.ThrowIfCancellationRequested();
                    try
                    {
                        Console.WriteLine(itm);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                var lst2 = bag.GetConsumingEnumerable(tokenSource.Token).ToList();
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.ToString()); 
            }
        }

        private static async Task AddItems(BlockingCollection<int> bag, CancellationTokenSource ts)
        {
            await await Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Task.Delay(5000);
                    var rdm = new Random();
                    var step = rdm.Next(1, 100);
                    Console.WriteLine($"-------------------{step}-------------------");
                    for (int i = 0; i < step; i++)
                    {
                        bag.Add(i);
                    }
                    if (step < -1)
                    {
                        ts.Cancel();
                        break;
                    }
                }
            });
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

        public static async Task<int> TaskAwait_Test(SimpleModel? model = null)
        {
            int i = 0;
            i = await Task.Run(async () =>
            {
                Console.WriteLine("In <-");
                await Task.Delay(5000);
                Console.WriteLine("out ->");
                if (model != null)
                    model.Name = $"Name - {model.Id}";

                throw new Exception($"End of {nameof(TaskAwait_Test)} - model.Id : {model?.Id}");
                return 3;
            });//.ConfigureAwait(false);
            Console.WriteLine($"Ended - {i}");
            //throw new Exception($"End of {nameof(TaskAwait_Test)} - {model?.Id}");
            return i;
        }

        public static async Task Parallel_ForEach_Test()
        {
            var list = new List<SimpleModel>() { new SimpleModel { Id = 1 }, new SimpleModel { Id = 2 }, new SimpleModel { Id = 3 } };
            ParallelLoopResult rsl;
            try
            {
                await Task.Run(() =>
                {
                    rsl = Parallel.ForEach(list, x =>
                    {
                        //await TaskAwait_Test();
                        //await TaskAwait_Test(x).ConfigureAwait(false);
                        //TaskAwait_Test(x).ConfigureAwait(false).GetAwaiter().GetResult();
                        //TaskAwait_Test(x).Wait();
                        _ = TaskAwait_Test(x).Result;
                    });
                });
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            await Console.Out.WriteLineAsync("Run ending Point 1");

            await Task.Delay(15000);
        }

        public static async Task ThreadThrowException_Test()
        {
            Task<int> tsk = null;
            try
            {
                tsk = TaskAwait_Test(new SimpleModel { Id = 100 });
            }
            catch(Exception ex)
            {

            }
            try
            {
                _ = await tsk;
            }
            catch(Exception ex)
            {

            }
        }

        public async static Task ContinueWith_Test()
        {
            var tsk = Task.Run(() => { return 1; });
            var x = await await tsk.ContinueWith( tsk => { throw new Exception("No Name"); return tsk.Result; })
                .ContinueWith( tsk => {
                    //throw new Exception("Second No Name");
                }, TaskContinuationOptions.OnlyOnFaulted)
                .ContinueWith(async tsk => {
                    try
                    {
                        await tsk;
                        return 2;
                    }
                    catch
                    {
                        return 3;
                    }
                })
                .ContinueWith(async tsk => { 
                    return await await tsk; 
                });
            await Console.Out.WriteLineAsync($"return value: {x}");
        }
    }
}