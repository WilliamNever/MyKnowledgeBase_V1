using System.Text.RegularExpressions;
using System.Web;

namespace GridSearchModels.Models
{
    public class UrlPart
    {
        private string OriPart { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string ColumnName { get; set; }
        public string Relation { get; set; }
        public int KeyPartCount { get; set; }
        private string? GridName { get; set; }

        public UrlPart(string part) : this(part, null)
        { }
        public UrlPart(string part, string? gidName)
        {
            OriPart = part;
            GridName = gidName;

            ToSeparateValues();
        }

        public void ToSeparateValues()
        {
            var Part = OriPart;
            if (!string.IsNullOrEmpty(GridName))
            {
                var reg = new Regex($"^({GridName}-)", RegexOptions.IgnoreCase);
                Part = reg.Replace(OriPart, "", 1, 0);
            }

            var leftAndRightPart = Part?.Split('=') ?? Array.Empty<string>();
            if (leftAndRightPart.Length < 1) return;

            Key = leftAndRightPart[0];
            if (leftAndRightPart.Length > 1) Value = HttpUtility.UrlDecode(leftAndRightPart[1] ?? "");
            var skmprt = Key.Split(new char[] { '-' }, 2);
            KeyPartCount = skmprt.Length;
            ColumnName = KeyPartCount > 0 ? skmprt[0] : "";
            Relation = KeyPartCount > 1 ? skmprt[1] : "";

            if (Key.Equals("sort", StringComparison.OrdinalIgnoreCase))
            {
                var vls = Value?.Replace("+", " ").Split(' ');
                if (vls != null && vls.Length > 0)
                {
                    ColumnName = vls[0];
                    Relation = vls.Length > 1 ? vls[1] : "";
                }
            }
        }
    }
}
