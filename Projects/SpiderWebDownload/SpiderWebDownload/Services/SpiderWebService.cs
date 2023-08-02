//#define debug

using SpiderWebDownload.Interfaces;
using SpiderWebDownload.Models;
using SpiderWebDownload.Settings;
using System.Text.RegularExpressions;

namespace SpiderWebDownload.Services
{
    public class SpiderWebService : IServices
    {
        private CommandLineParams settings;
        public SpiderWebService(CommandLineParams settings)
        {
            this.settings = settings;
        }

        public async Task<DownloadedFileModel[]> RunDownloadAsync()
        {
            List<string> lnks = new();
#if !debug
            HttpClient client = new HttpClient();
            var resp = await client.GetAsync(settings.Uri);
            if (!resp.IsSuccessStatusCode)
                throw new Exception(resp.StatusCode.ToString());
            var mainPage = settings.PageEncoding.GetString(await resp.Content.ReadAsByteArrayAsync());

            var tagIdfilter = string.IsNullOrEmpty(settings.ContainTagID) ? "" : $" {settings.ContainTagID}";
            Regex regexScope = new Regex(@$"<{settings.ContainTag}{tagIdfilter}[\W\w]+?</{settings.ContainTag}>", RegexOptions.IgnoreCase);
            Regex regexALnk = new Regex(@$"<a[\W\w]+?>", RegexOptions.IgnoreCase);
            var TagScope = regexScope.Matches(mainPage).OfType<Match>().Select(x => x.Value);
            foreach (var item in TagScope)
            {
                lnks.AddRange(regexALnk.Matches(item).OfType<Match>().Select(x => x.Value));
            }
#endif

#if debug
            lnks = new List<string> { "<a href=\"/novel/3579/180921.html\" class=\"header-back jsBack\">" };
#endif

            var LnkDics = lnks.Select(l =>
            {
                var tparts = l?.Replace(">", " >").Split(' ').FirstOrDefault(x => x.StartsWith("href=", StringComparison.OrdinalIgnoreCase));
                var url = tparts?.Replace("href=", "", StringComparison.OrdinalIgnoreCase);
                return url?.Trim('"').Trim('\'');
            }).ToList();

            SemaphoreSlim sl = new SemaphoreSlim(15, 30);
            List<Task<List<DownloadedFileModel>>> tasks = new();
            for (int i = 0; i < lnks.Count; i++)
            {
                var lnk = LnkDics[i];
                tasks.Add(DownLoadFileAsync(lnk, (settings.Sub.HasValue ? (settings.Sub.Value + 1) * i : i), settings, sl, true));
            }
            var rslts = await Task.WhenAll(tasks);
            return rslts.SelectMany(x => x.Select(y => y)).ToArray();
        }

        private async Task<List<DownloadedFileModel>> DownLoadFileAsync(string? lnk, int i, CommandLineParams settings, SemaphoreSlim sl, bool isMainPage = true)
        {
            var rsl = new List<DownloadedFileModel>();
            try
            {
                await sl.WaitAsync();
                HttpClient client = new HttpClient();
                string? url = CreateUrl(lnk, settings);
                var resp = client.GetAsync(url);

                if (!Directory.Exists(settings.OuputFolder)) Directory.CreateDirectory(settings.OuputFolder);
                string fileName = $"{settings.OuputFolder}{i.ToString().PadLeft(settings.SeqLength, '0')}_{Path.GetFileName(lnk)}";
                using (var fs = File.Open(fileName, FileMode.CreateNew))
                {
                    var bytes = await (await resp).Content.ReadAsByteArrayAsync();
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
                }
                rsl.Add(new DownloadedFileModel { Index = i, FileName = fileName });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{lnk} was failed to download.");
                rsl.Add(new DownloadedFileModel { Index = i, FileName = null });
                return rsl;
            }
            finally
            {
                sl.Release();
            }

            if (isMainPage && settings.Sub.HasValue)
            {
                var subIndex = i + 1;
                List<Task<List<DownloadedFileModel>>> tasks = new();
                for (int sp = 0; sp < settings.Sub.Value; sp++)
                {
                    var subEx = Path.GetExtension(lnk);
                    var fnNoEx = Path.GetFileNameWithoutExtension(lnk);
                    var fpath = Path.GetDirectoryName(lnk);
                    var subLnk = $"{Path.Combine(fpath ?? "", fnNoEx ?? "")}{string.Format(settings.SubFileNameFormat, sp + settings.SubStartNumber)}{subEx}".Replace("\\","/");
                    tasks.Add(DownLoadFileAsync(subLnk, sp + subIndex, settings, sl, false));
                }
                var subfiles = await Task.WhenAll(tasks);
                rsl.AddRange(subfiles.SelectMany(x => x.Select(y => y)));
            }
            return rsl;

        }

        private string? CreateUrl(string? lnkUri, CommandLineParams settings)
        {
            string? lnk = lnkUri?.Replace("\\", "/");
            string? url;
            if (lnk?.StartsWith("/") ?? false)
            {
                url = $"{settings.Uri?.Scheme}://{settings.Uri?.Host}/{lnk}";
            }
            else if (lnk?.StartsWith($"{settings.Uri?.Scheme}://{settings.Uri?.Host}") ?? false)
            {
                url = lnk;
            }
            else
            {
                url = $"{settings.Uri?.Scheme}://{settings.Uri?.Host}{string.Concat(settings.Uri?.Segments[0..^1] ?? Array.Empty<string>())}{lnk}";
            }
            return url;
        }
    }
}
