using SpiderWebDownload.Models;
using SpiderWebDownload.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpiderWebDownload.Services
{
    public class FileService
    {
        private CommandLineParams argsSettings;
        public FileService(CommandLineParams argsSettings)
        {
            this.argsSettings = argsSettings;
        }
        public string CreateMergedFile(DownloadedFileModel[] infiles)
        {
            List<DownloadedFileModel> files = infiles.ToList();
            if (argsSettings.From.HasValue)
            {
                files = files.Where(x => x.Index >= argsSettings.From.Value).ToList();
            }
            if (argsSettings.To.HasValue)
            {
                files = files.Where(x => x.Index <= argsSettings.To.Value).ToList();
            }

            string fileName = $"{argsSettings.OuputFileName}";
            using (var fs = File.Open(fileName, FileMode.Create))
            {
                foreach (var file in files.OrderBy(x => x.Index))
                {
                    using (var rfs = File.OpenRead(file.FileName ?? throw new FileNotFoundException($"{file.FileName} does not exists.")))
                    {
                        rfs.CopyTo(fs);
                        rfs.Flush();
                        fs.Flush();
                    }
                }
                fs.Flush();
            }
            return fileName;
        }

        public void UpdateArgsSettings(CommandLineParams argsSettings)
        {
            this.argsSettings = argsSettings;
        }

        public DownloadedFileModel[] ReadFilesFromOutputFolder()
        {
            var opf = argsSettings.OuputFolder;
            if (!Directory.Exists(opf)) Directory.CreateDirectory(opf);
            var files = Directory.GetFiles(opf);
            return files.Select(x => GetDlFModel(x)).ToArray();
        }

        private DownloadedFileModel GetDlFModel(string filefullName)
        {
            var fn = Path.GetFileName(filefullName);
            int index = -1;
            var fnSplit = fn?.Split('_');
            if (fnSplit != null && fnSplit.Length > 0 && int.TryParse(fnSplit[0], out int tmp))
            {
                index = tmp;
            }
            return new DownloadedFileModel
            {
                Index = index,
                FileName = filefullName
            };
        }

        public async Task CreateTitleListAsync()
        {
            string contents;
            byte[] bytes;
            var exts = Path.GetExtension(argsSettings.MakeListSourceFile);
            var listFileName = $"{argsSettings.MakeListSourceFile[0..^exts.Length]}_titleList{exts}";
            using (FileStream sr = new FileStream(argsSettings.MakeListSourceFile, FileMode.Open))
            {
                bytes = new byte[sr.Length];
                await sr.ReadAsync(bytes, 0, bytes.Length);
            }

            contents = argsSettings.PageEncoding.GetString(bytes);
            Regex regex = new Regex(argsSettings.TitleFilterRegexExpress);
            var mses = regex.Matches(contents);
            using (var sw = new StreamWriter(listFileName, false, Encoding.UTF8))
            {
                foreach (Match mse in mses)
                {
                    sw.WriteLine(mse.Value);
                }
                await sw.FlushAsync();
            }
            Console.WriteLine($"Created title list files...");
        }

    }
}
