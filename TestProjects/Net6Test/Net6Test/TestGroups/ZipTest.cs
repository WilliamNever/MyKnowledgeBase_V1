using System.IO.Compression;

namespace Net6Test.TestGroups
{
    public class ZipTest
    {
        public static void ZipFolder()
        {
            Console.WriteLine(DateTime.Now);
            ZipFile.CreateFromDirectory(@"D:\WQPersonal\EBooks", @"D:\Temp\Minted\Archive\aaa.zip",
                CompressionLevel.Fastest, true);
            Console.WriteLine(DateTime.Now);
        }
        public static void ZipToFile()
        {
            using (FileStream zipToOpen = new FileStream(@"D:\Temp\Minted\Archive\release.zip", FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry readmeEntry = archive.CreateEntry("Readme.txt");
                    using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                    {
                        writer.WriteLine("Information about this package.");
                        writer.WriteLine("========================");
                    }
                }
            }
        }
    }
}
