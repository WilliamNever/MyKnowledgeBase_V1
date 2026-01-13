namespace Net6Test.TestGroups
{
    public class MessTests
    {
        public async static Task T1()
        {
            var dtnUtc = DateTime.UtcNow;
            var dtUtc = dtnUtc.Subtract(TimeSpan.FromMinutes(10));
            Dictionary<string, KeyValuePair<string, int>> failedShipments = new();
            string key = string.Empty;
            string key1 = null!;
            try
            {
                failedShipments.ContainsKey(key);
                failedShipments.Add(key, new KeyValuePair<string, int>(key, 1));

                failedShipments.ContainsKey(key1);
                failedShipments.Add(key1, new KeyValuePair<string, int>(key, 1));
            }
            catch (Exception ex)
            {
            }
            finally
            { 
            }
        }
    }
}
