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
            Task.WaitAll(InitialTest());
        }

        private async Task InitialTest()
        {
            await Console.Out.WriteLineAsync("Hello - ");
        }
    }
}
