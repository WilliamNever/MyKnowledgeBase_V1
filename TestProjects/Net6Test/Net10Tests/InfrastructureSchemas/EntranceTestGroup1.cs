namespace Net10Tests.InfrastructureSchemas
{
    public class EntranceTestGroup1: EntranceBase
    {
        public override async Task MainRunAsync()
        {
            Console.WriteLine($"Enter MainRunAsync - {DateTime.Now}");
        }
    }
}
