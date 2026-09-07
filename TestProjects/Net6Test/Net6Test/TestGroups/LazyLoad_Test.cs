namespace Net6Test.TestGroups
{
    public static class LazyLoad_Test
    {
        public async static Task LazyLoad()
        {
            var Ltsks = new List<Lazy<Task<int>>>();
            var tsks = new List<Task<int>>();

            //var lzyObj = new Lazy<Task<int>>(() => LazyLoadTest(2), LazyThreadSafetyMode.ExecutionAndPublication);
            //var rsl = await lzyObj.Value;
            //Console.WriteLine(rsl);
            //lzyObj = new Lazy<Task<int>>(() => LazyLoadTest(4), LazyThreadSafetyMode.ExecutionAndPublication);
            //rsl = await lzyObj.Value;
            //Console.WriteLine(rsl);


            //for (int j = 0; j < 5; j++)
            //{
            //    var num = 1 << j;
            //    tsks.Add(new Lazy<Task<int>>(() => LazyLoadTest(num), LazyThreadSafetyMode.None));
            //}

            Console.WriteLine($"Main - {Thread.CurrentThread.ManagedThreadId}");

            /// testings for thread safety
            var lzyClass = new Lazy<CtentObj>(LazyThreadSafetyMode.ExecutionAndPublication);
            //var lzyClass = new Lazy<CtentObj>(LazyThreadSafetyMode.PublicationOnly);
            //var lzyClass = new Lazy<CtentObj>(LazyThreadSafetyMode.None);

            for (int i = 0; i < 5; i++)
            {
                tsks.Add(Task.Run(() => {
                    var lobj = lzyClass.Value;
                    Console.WriteLine(lobj.ToString());
                    Console.WriteLine($"Sub - {Thread.CurrentThread.ManagedThreadId}");
                    return Task.FromResult(lobj.ThreadId); 
                }));
            }

            var nums = await Task.WhenAll(Ltsks.Select(t => t.Value));
            int[] numsTsk;
            try
            {
                numsTsk = await Task.WhenAll(tsks);
            }
            catch (Exception ex)
            {
            }
            var lobj = lzyClass.Value;
        }
        public async static Task<int> LazyLoadTest(int LimitedNum = -1)
        {
            int idx = 1;
            //var lzyObj = new Lazy<Task<int>>(async () =>
            //{
            //    Console.WriteLine($"Enter - {DateTime.Now}");
            //    await Task.Delay(3_000);

            //    Console.WriteLine($"Exit - {DateTime.Now}");
            //    return idx;
            //});

            var lzyObj = new LazyObj(() => idx);
            var lzyClass = new Lazy<CtentObj>(LazyThreadSafetyMode.PublicationOnly);
            idx = 1 << 0;
            if ((idx & LimitedNum) > 0)
                _ = lzyObj.Value;
            Console.WriteLine($"Main Start Working - {LimitedNum} - 5sec - {DateTime.Now}");
            await Task.Delay(5_000);
            Console.WriteLine($"Main enter delay - {LimitedNum} - 5sec - {DateTime.Now}");
            await Task.Delay(5_000);
            Console.WriteLine($"Main exits delay - {LimitedNum} - 5sec - {DateTime.Now}");
            
            idx = 1 << 1;
            if ((idx & LimitedNum) > 0)
            {
                _ = lzyClass.Value;
                _ = lzyObj.Value;

                Console.WriteLine($"Result rsl - {LimitedNum} - " +
                    $"{await lzyObj.Value.WaitAsync(CancellationToken.None)}  -  {lzyClass.Value.InitialDateTime}");
            }

            idx = 1 << 2;
            if ((idx & LimitedNum) > 0)
            {
                _ = lzyObj.Value;
                Console.WriteLine($"Result rsl1 - {LimitedNum} - " +
                    $"{await lzyObj.Value.WaitAsync(CancellationToken.None)}  -  {lzyClass.Value.InitialDateTime}");
            }

            await Task.Delay(5_000);
            idx = 1 << 3;
            if ((idx & LimitedNum) > 0)
            {
                _ = lzyObj.Value;
                Console.WriteLine($"Result rsl2 - {LimitedNum} - " +
                    $"{await lzyObj.Value.WaitAsync(CancellationToken.None)}  -  {lzyClass.Value.InitialDateTime}");
            }

            Console.WriteLine($"Result rsl2 - {LimitedNum} - " +
                    $"{await lzyObj.Value.WaitAsync(CancellationToken.None)}  -  {lzyClass.Value.InitialDateTime}");
            return LimitedNum;
        }
    }

    public class LazyObj {
        private Lazy<Task<int>> lzyObj;
        public LazyObj(Func<int> idx)
        {
            lzyObj = new Lazy<Task<int>>(async () =>
            {
                Console.WriteLine($"Enter {idx()} - {DateTime.Now}");
                await Task.Delay(2_000);

                Console.WriteLine($"Exit {idx()} - {DateTime.Now}");
                return idx();
            });
        }
        public Task<int> Value => lzyObj.Value;
    }

    public class CtentObj
    {
        public int ThreadId {  get; private set; }
        public string InitialDateTime {  get; set; }
        public CtentObj()
        {
            InitialDateTime = $"{DateTime.Now}";
            ThreadId = Thread.CurrentThread.ManagedThreadId;
            if (ThreadId > 8)
            {
                Console.WriteLine($"Over 8 value - {ThreadId}");
                throw new Exception("Threads Over limits!");
            }
        }
        public override string ToString()
        {
            return $"ToString / {ThreadId} - {InitialDateTime}";
        }
    }
}
