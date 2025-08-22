namespace Net6Test.TestGroups
{
    public class DelegateFuncAction_Tests
    {
        protected delegate int DelGetIntGrp() ;
        public async static Task DelegateGrpTest()
        {
            var nexp = new NothingProcessedException("No", "empty", true);
            DelGetIntGrp grp = new(() => 1);

            //var ev = new EventHandler((obj, args) => { 
            var ev = new EventHandler<EvArgs>((obj, args) => { 
                Console.WriteLine($"1 - {args.CallerName}");
            });
            ev += (obj, args) => { 
                Console.WriteLine($"2"); 
            };
            ev.Invoke(null, new EvArgs("C1"));

            string str = "ori";
            bool aBool = false;

            Func<string, Task<string>> oa = async (st) => { 
                st = "XXXX";
                return st;
            };
            var mm = await oa(str);
            mm = await AA(str);

            Func<string, bool, (string, bool)> func = (s, b) => { return ("name", true); };
            (str, aBool) = func.Invoke(str, aBool);
        }

        private static async Task<string> AA(string st)
        {
            st = "XXXX1";
            return st;
        }
    }

    public class EvArgs : EventArgs
    {
        public string CallerName { get; set; }
        public EvArgs(string callerName)
        {
            CallerName = callerName;
        }
    }
    public class NothingProcessedException : Exception
    {
        public bool isProcessSuccessful { get; set; } = false;
        public string Reason { get; protected set; }
        public NothingProcessedException(bool ispassed = false) : base()
        {
            Reason = "UnKnown".ToUpper();
            isProcessSuccessful = ispassed;
        }
        public NothingProcessedException(string message, bool ispassed = false) : base(message)
        {
            Reason = "UnKnown".ToUpper();
            isProcessSuccessful = ispassed;
        }
        public NothingProcessedException(string? Reason, string message, bool ispassed = false) : this(message, ispassed)
        {
            this.Reason = (Reason ?? "UnKnown").ToUpper();
        }
    }
}
