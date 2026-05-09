using Net6Test.Models;
using System.Collections.Concurrent;

namespace Net6Test.TestGroups
{
    public class ConcurrentClassTests
    {
        public async static Task ConcurrentDictionary1_Test()
        {
            var cd = new ConcurrentDictionary<string, SmpModel>();
            var smp = new SmpModel() { Key = "AName" };
            cd.AddOrUpdate(smp.Key, smp, (key, vle) => { 
                ++vle.Attemp; 
                return vle; 
            });

            smp = new SmpModel() { Key = "AName", Attemp = 10 };
            cd.AddOrUpdate(smp.Key, smp, (key, vle) => { 
                ++vle.Attemp; 
                return vle; 
            });
            smp = new SmpModel() { Key = "AName", Attemp = 100 };
            cd.AddOrUpdate(smp.Key, smp, (key, vle) => { 
                return smp; 
            });
        }

        public async static Task ConcurrentDictionary_Test()
        {
            CancellationTokenSource ts = new CancellationTokenSource();
            var tsSub1 = CancellationTokenSource.CreateLinkedTokenSource(ts.Token);
            ConcurrentDictionary<int, Task> bags = new();
            bags.TryAdd(1, Task.Run(async () => await funcMain("xxx", tsSub1.Token)));
            await Task.Delay(3 * 1000);
            tsSub1.Cancel();
            await Task.Delay(5 * 1000);
            var added = bags.TryAdd(1, Task.Run(async () => await funcMain("vvv", ts.Token)));
            await Task.Delay(5 * 1000);
            ts.Cancel();
            await Task.Delay(3 * 1000);
        }
        
        private static Func<string, CancellationToken, Task> funcMain = async (name, token) =>
        {
            var ss = Path.Combine("c:\\aa\\", "abc", "cc.txt");
            try
            {
                while (!token.IsCancellationRequested)
                {
                    Console.WriteLine($"{name} - {DateTime.Now} - {Thread.CurrentThread.ManagedThreadId}");
                    await Task.Delay(1000);
                }
                Console.WriteLine($"Task - {name} Exited.");
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString()); 
            }
        };
    }
}
