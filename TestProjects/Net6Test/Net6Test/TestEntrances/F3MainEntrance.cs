using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestEntrances
{
    public class F3MainEntrance : EntranceBase
    {
        public override void MainRun()
        {
            InitialTest().Wait();
        }

        private async Task InitialTest()
        {
            await Console.Out.WriteLineAsync(string.Format("-{0}-", "xxx", 1, 2, 3));
        }
    }
}
