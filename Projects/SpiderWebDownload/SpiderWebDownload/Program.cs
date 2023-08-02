using SpiderWebDownload;
using SpiderWebDownload.Settings;

{
    if (args.Length == 0)
    {
        AppMain.ShowUsages();
        return;
    }
    CommandLineParams argsSettings;
    try
    {
        argsSettings = CommandLineParams.ReadArgs(args);
        Console.WriteLine($"Output folder - ");
        Console.WriteLine($"{Path.GetFullPath(argsSettings.OuputFolder)}");
    }
    catch (Exception ex)
    {
        AppMain.ShowUsages();
        return;
    }

    await AppMain.MainAsync(argsSettings);


    Action EndInfor = () =>
    {
        Console.WriteLine();
        Console.WriteLine($"Any key to exit......");
        _ = Console.ReadKey();
    };

    EndInfor();
}