using NetCoreTest.BaseClassModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreTest.TestCases
{
    public class TaskRunTemplateTest : TestCaseBaseModel
    {
        public async override Task ExcuteAsync()
        {
            var mess = await NetStandardLibrary.TaskHelpers.TaskRunTemplate.RunAsync(
                            () =>
                            {
                                return GetHellowName("Morning");
                            }
                            );
            Console.WriteLine(mess);
            await base.ExcuteAsync();
        }
        private static string GetHellowName(string Name)
        {
            return $"Hello {Name}!";
        }
    }
}
