using NetCoreTest.BaseClassModels;
using NetCoreTest.TestCases;
using System;
using System.Threading.Tasks;

namespace NetCoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCaseBaseModel testCase
                //= new TaskRunTemplateTest();
                = new ReadingCsvStringTest();

            Task.WaitAll(testCase.ExcuteAsync());
        }
    }
}
