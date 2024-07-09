namespace F8C.Core.Consts
{
    public class ConstDefine
    {
        public const string Positive = "\u2500\u2500\u2500";
        public const string Negative = "\u2500  \u2500";

        public const int Full3Positive = 7;
        public const int Full6Positive = 63;
        public static readonly int[] BitNums = new int[] { 32, 1, 2, 4, 8, 16 };
        public static readonly Dictionary<int, int> Dics
            = new()
            {
                { 0, 0 } ,
                { 1, 7 } ,
                { 2, 3 } ,
                { 3, 5 } ,

                { 4, 1 } ,
                { 5, 6 } ,
                { 6, 2 } ,
                { 7, 4 } ,
            };
        public static readonly Dictionary<int, string> DicName
            = new()
            {
                { 0, "坤" } ,
                { 7, "乾" } ,
                { 3, "兑" } ,
                { 5, "离" } ,

                { 1, "震" } ,
                { 6 , "巽"} ,
                { 2 , "坎"} ,
                { 4 , "艮"} ,
            };
        public static readonly Dictionary<int, string> DicSymbol
            = new()
            {
                { 0, "地" } ,
                { 7, "天" } ,
                { 3, "泽" } ,
                { 5, "火" } ,

                { 1, "雷" } ,
                { 6 , "风"} ,
                { 2 , "水"} ,
                { 4 , "山"} ,
            };


        public static int GetDT12(DateTime dateTime)
        {
            var HH = dateTime.Hour;
            return (((HH & 1) > 0 ? (HH + 1) : HH) % 24 / 2) + 1;
        }
        public static int GetDT12(int HH) =>
            (((HH & 1) > 0 ? (HH + 1) : HH) % 24 / 2) + 1;

        public static int BeginRsl(int sh, int dn) => Dics[sh & Full3Positive] << 3 | Dics[dn & Full3Positive];
        public static int ChangedRsl(int begin, int rateNum) => begin ^ BitNums[rateNum % 6];
        public static int ExchangeRsl(int begin, int change)
        {
            int src = begin;
            if (begin == 0 || begin == Full6Positive) src = change;
            return (src >> 2 & Full3Positive) << 3 | (src >> 1 & Full3Positive);
        }
    }
}