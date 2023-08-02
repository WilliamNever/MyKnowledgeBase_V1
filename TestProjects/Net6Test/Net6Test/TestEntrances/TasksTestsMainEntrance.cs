using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestEntrances
{
    public class TasksTestsMainEntrance : EntranceBase
    {
        public override void MainRun()
        {
            Task.WaitAll(TaskTest_1());
        }

        private async Task TaskTest_1()
        {
            var slim = new SemaphoreSlim(1);
            var cancel = new CancellationTokenSource();
            var token = cancel.Token;
            List<Task<string>> tasks = new();
            foreach (var str in new string?[] { " Here ", null, "  " })
            {
                tasks.Add(TestBodyContentAsync(str, slim, cancel));
            }
            string[] results;
            try
            {
                results = await Task.WhenAll(tasks);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #region MyRegion

        #endregion
        private async Task<string> TestBodyContentAsync(string? str, SemaphoreSlim slim, CancellationTokenSource cancel)
        {
            try
            {
                if (cancel.IsCancellationRequested)
                {
                    return "End";
                }

                await slim.WaitAsync();
                var tmp = str!.Trim();
                return tmp;
            }
            catch (Exception ex)
            {
                cancel.Cancel();
                throw new Exception($"Value {str ?? "NULL"} is invalid.");
            }
            finally
            {
                slim.Release();
            }
        }
    }
}
