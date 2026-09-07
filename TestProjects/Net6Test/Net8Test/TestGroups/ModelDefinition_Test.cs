
using Net8Test.Models;

namespace Net8Test.TestGroups
{
    public class ModelDefinition_Test
    {
        public static async Task Definition_Test()
        {
            var tma = new TestModelA {
                Name = "test",
                Description = "Description"
            };
            tma.Name = "test1";

            var tmb = new TestModelA("xxx");
        }
    }
}
