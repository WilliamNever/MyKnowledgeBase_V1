namespace Net6Test.Services
{
    public class CalculateService
    {
        public static async Task LiquidMixedTemperature()
        {
            decimal lowTemper = 20;
            decimal lowPart = 1;
            decimal highTemper = 100;
            decimal highPart = 10;
            Console.WriteLine($"");
            for (int i = 1; i <= highPart; i++) {
                var tLiquid = (lowTemper * lowPart) + (highTemper * i);
                var mixedTmper = tLiquid / (lowPart + i);
                Console.WriteLine($"Low liquid - {lowTemper} * {lowPart}");
                Console.WriteLine($"High liquid - {highTemper} * {i}");
                Console.WriteLine($"Mixed Temperature - {mixedTmper}");
                Console.WriteLine("-------------------");
            }
        }
    }
}
