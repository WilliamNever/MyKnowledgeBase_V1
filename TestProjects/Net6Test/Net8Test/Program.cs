using Net8Test;

var enter = new Entrance_1();
await enter.Entrance();


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