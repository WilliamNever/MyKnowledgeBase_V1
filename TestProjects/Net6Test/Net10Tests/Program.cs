








using Net10Tests.InfrastructureSchemas;

EntranceBase main
= new EntranceTestGroup1();

await main.MainRunAsync();
















#region End Part

Action EndInfor = () =>
{
    Console.WriteLine();
    Console.WriteLine($"Any key to exit......");
    while (true)
    {
        var readKey = Console.ReadKey();
        if (readKey.Key == ConsoleKey.Enter) break;
        else Console.Write($" - {readKey.KeyChar} / ");
    }
};

EndInfor();

#endregion