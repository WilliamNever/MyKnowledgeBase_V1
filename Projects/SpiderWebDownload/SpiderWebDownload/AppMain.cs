using SpiderWebDownload.Models;
using SpiderWebDownload.Services;
using SpiderWebDownload.Settings;
using System.Text;
using System.Text.RegularExpressions;

namespace SpiderWebDownload
{
    public static class AppMain
    {
        public static async Task MainAsync(CommandLineParams argsSettings)
        {
            DownloadedFileModel[]? dlfiles = null;
            var spiderWebService = new SpiderWebService(argsSettings);
            var fileService = new FileService(argsSettings);
            try
            {
                if (argsSettings.Uri != null)
                {
                    dlfiles = await spiderWebService.RunDownloadAsync();
                    if (dlfiles.Any(x => x.FileName == null)) return;
                }
                if (argsSettings.IsOutputWholeFile)
                {
                    if (dlfiles == null)
                    {
                        dlfiles = fileService.ReadFilesFromOutputFolder();
                    }
                    argsSettings.MakeListSourceFile = fileService.CreateMergedFile(dlfiles);
                    fileService.UpdateArgsSettings(argsSettings);
                }
                if (argsSettings.ToListTitle)
                    await fileService.CreateTitleListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine($"Finished downloading...");
        }

        #region Assistant Methods

        public static Action ShowUsages = () =>
        {
            Console.WriteLine("Usages - ");
            Console.WriteLine("SpiderWebDownload [-url url] [-tag link_container] [-o output folder] [-m] [-f output file name] [-l] [-enc GBK]");
            Console.WriteLine("SpiderWebDownload -url https://xxx.xx.com/index.htm -tag tr -m -f output.txt -l -enc GBK");
            Console.WriteLine("Surported Args -");
            Console.WriteLine("-url, the list page with links that need to download");
            Console.WriteLine("-tag, default is html");
            Console.WriteLine("-tagid, default is empty string");

            Console.WriteLine(@"-o, output folder, default is .\DateTime.Now.ToString(""yyyyMMddHHmmss"")\");
            Console.WriteLine(@"-m, to merge downloaded files");
            Console.WriteLine(@"-from, when merging files to indicate the beginning page number");
            Console.WriteLine(@"-to, when merging files to indicate the ending page number");

            Console.WriteLine(@"-f, the merged file name");
            Console.WriteLine(@"-l, to create title list file");
            Console.WriteLine(@"-tflt, the Regex express that pickup the titles");
            Console.WriteLine(@"-enc, the encoding of the web page, default is UTF8");
            Console.WriteLine(@"-sub, it is an integer, which indicates how many sub pages will be tried to donwload.");

            Console.WriteLine(@"-sf, a string, which indicates the format of the sub file name. Such as '{0:D2}', '1' will be '01', when the length is less that 2. Default value is '_{0}'.");
            Console.WriteLine(@"-stt, a integer, which indicates the start page number, defautl is 0.");
        };

        #endregion
    }
}
