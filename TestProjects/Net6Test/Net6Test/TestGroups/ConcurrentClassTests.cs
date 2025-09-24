using System.Collections.Concurrent;

namespace Net6Test.TestGroups
{
    public class ConcurrentClassTests
    {
        public static Func<string, CancellationToken, Task> funcMain = async (name, token) =>
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
    }
}
