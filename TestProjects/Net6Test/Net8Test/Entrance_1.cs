using Net8Test.TestGroups;

namespace Net8Test
{
    public class Entrance_1 : EntranceBase
    {
        public override async Task Entrance()
        {
            //await ModelDefinition_Test.Definition_Test();

            await Resilience_Http_Retry_test.ReTryCall_Test(provider);
        }
    }
}
