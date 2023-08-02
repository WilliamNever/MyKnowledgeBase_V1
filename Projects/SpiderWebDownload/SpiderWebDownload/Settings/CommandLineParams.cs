using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderWebDownload.Settings
{
    public class CommandLineParams
    {
        public string Url { get; set; } = "";
        public string ContainTag { get; set; } = "";
        public string ContainTagID { get; set; } = "";
        public string OuputFolder { get; set; } = "";
        public string OuputFileName { get; set; } = "";
        public string MakeListSourceFile { get; set; } = "";
        public Uri? Uri { get; set; }
        public int SeqLength { get; set; } = 5;
        public bool IsOutputWholeFile { get; set; } = false;

        public bool ToListTitle { get; set; } = false;
        public string TitleFilterRegexExpress { get; set; } = "";

        public int? From { get; set; }
        public int? To { get; set; }
        /// <summary>
        /// sub pages to download
        /// </summary>
        public int? Sub { get; set; } = null;
        public int SubStartNumber { get; set;} = 0;
        public string SubFileNameFormat { get; set; } = "";

        public Encoding PageEncoding { get; set; } = Encoding.UTF8;

        public static CommandLineParams ReadArgs(string[] args)
        {
            var fn = DateTime.Now.ToString("yyyyMMddHHmmss");
            var url = GetOptionalValue(args, "-url", "");
            var outputFolder = $"{GetOptionalValue(args, "-o", $".\\{fn}\\").TrimEnd('\\')}\\";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var from = GetOptionalValue(args, "-from", "");
            var to = GetOptionalValue(args, "-to", "");
            var sub = GetOptionalValue(args, "-sub", null);
            var sttNumber = GetOptionalValue(args, "-stt", "0");
            return new CommandLineParams
            {
                Url = url,
                ContainTag = GetOptionalValue(args, "-tag", "html"),
                OuputFolder = outputFolder,
                Uri = string.IsNullOrEmpty(url) ? null : new Uri(url),
                IsOutputWholeFile = args.Any(x => x.Equals("-m", StringComparison.OrdinalIgnoreCase)),
                OuputFileName = $"{outputFolder}{GetOptionalValue(args, "-f", $"Merged_{fn}.txt")}",
                MakeListSourceFile = $"{GetOptionalValue(args, "-f", "")}",
                ToListTitle = args.Any(x => x.Equals("-l", StringComparison.OrdinalIgnoreCase)),
                TitleFilterRegexExpress = GetOptionalValue(args, "-tflt", "<title[\\w\\W]+?</title>"),
                PageEncoding = Encoding.GetEncoding(GetOptionalValue(args, "-enc", "UTF-8")),
                ContainTagID = GetOptionalValue(args, "-tagid", ""),
                From = int.TryParse(from, out int fr) ? fr : null,
                To = int.TryParse(to, out int tS) ? tS : null,
                Sub = int.TryParse(sub, out var tsub) ? tsub : null,
                SubFileNameFormat = GetOptionalValue(args, "-sf", "_{0}"),
                SubStartNumber = int.TryParse(sttNumber, out var stt) ? stt : 0
            };
        }
        private static string? GetOptionalValue(string[] args, string op, string? defaultValue)
        {
            string? val = defaultValue;
            var option = args.FirstOrDefault(x => x.Equals(op, StringComparison.OrdinalIgnoreCase));
            if (option != null)
            {
                var index = Array.IndexOf(args, option);
                val = args[index + 1];
            }
            return val;
        }
    }
}
