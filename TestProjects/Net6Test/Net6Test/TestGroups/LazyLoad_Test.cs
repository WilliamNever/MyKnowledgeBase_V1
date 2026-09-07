namespace Net6Test.TestGroups
{
    public static class LazyLoad_Test
    {
        public async static Task LazyLoad()
        {
            int idx = 55;
            //var lzyObj = new Lazy<Task<int>>(async () =>
            //{
            //    Console.WriteLine($"Enter - {DateTime.Now}");
            //    await Task.Delay(3_000);

            //    Console.WriteLine($"Exit - {DateTime.Now}");
            //    return idx;
            //});

            var lzyObj = new LazyObj(() => idx);
            var lzyClass = new Lazy<CtentObj>();
            idx = 66;
            Console.WriteLine($"Main Start Working - 5sec - {DateTime.Now}");
            await Task.Delay(5_000);
            Console.WriteLine($"Main enter delay - 5sec - {DateTime.Now}");
            await Task.Delay(5_000);
            Console.WriteLine($"Main exits delay - 5sec - {DateTime.Now}");
            idx = 77;
            var obj = lzyClass.Value;
            var rsl = await lzyObj.Value.WaitAsync(CancellationToken.None);
            Console.WriteLine($"Result rsl - {rsl} - {obj.InitialDateTime}");
            idx = 88;
            var rsl1 = await lzyObj.Value.WaitAsync(CancellationToken.None);
            Console.WriteLine($"Result rsl1 - {rsl1} - {obj.InitialDateTime}");
            await Task.Delay(5_000);
            idx = 99;
            var rsl2 = await lzyObj.Value.WaitAsync(CancellationToken.None);
            Console.WriteLine($"Result rsl2 - {rsl2} - {obj.InitialDateTime}");
        }
    }

    public class LazyObj {
        private Lazy<Task<int>> lzyObj;
        public LazyObj(Func<int> idx)
        {
            lzyObj = new Lazy<Task<int>>(async () =>
            {
                Console.WriteLine($"Enter - {DateTime.Now}");
                await Task.Delay(2_000);

                Console.WriteLine($"Exit - {DateTime.Now}");
                return idx();
            });
        }
        public Task<int> Value => lzyObj.Value;
    }

    public class CtentObj
    {
        public string InitialDateTime {  get; set; }
        public CtentObj()
        {
            InitialDateTime = $"{DateTime.Now}";
        }
    }
}
