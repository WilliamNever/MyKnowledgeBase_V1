namespace Net6Test.TestGroups
{
    public class SemaphoreSlim_Test
    {
        public static async Task SemaphoreSlim_MaxCount_Test()
        {
            SemaphoreSlim slim = new SemaphoreSlim(1, 1);
            var isEnter = await slim.WaitAsync(TimeSpan.FromSeconds(3));
            slim.Release();
            try
            {
                slim.Release();
            }
            catch (Exception ex)
            {
            }
            finally { 
            }
        }
    }
}
