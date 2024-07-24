using F8C.Core.Models;

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
                { 1, 7 } ,
                { 2, 3 } ,
                { 3, 5 } ,
                { 4, 1 } ,

                { 5, 6 } ,
                { 6, 2 } ,
                { 7, 4 } ,
                { 0, 0 } ,
            };
        public static readonly Dictionary<int, SingleSymbol> DicName
            = new()
            {
                { 7, new SingleSymbol(1,"乾","西北","天") } ,
                { 3, new SingleSymbol(2,"兑","西","泽") } ,
                { 5, new SingleSymbol(3,"离","南","火") } ,
                { 1, new SingleSymbol(4,"震","东","雷") } ,

                { 6, new SingleSymbol(5,"巽","东南","风") } ,
                { 2, new SingleSymbol(6,"坎","北","水") } ,
                { 4, new SingleSymbol(7,"艮","东北","山") } ,
                { 0, new SingleSymbol(8,"坤","西南","地") } ,

            };

        public static Dictionary<GuaModel, GuaNameModel> GuaName
        {
            get => new() {
            { new(7,7), new(1,"乾为天","乾上乾下") },
            { new(7,6), new(0,"","") },
            { new(7,5), new(13,"天火同人","乾上离下") },
            { new(7,4), new(0,"","") },
            { new(7,3), new(10,"天泽履","乾上兑下") },
            { new(7,2), new(6,"天水讼","乾上坎下") },
            { new(7,1), new(0,"","") },
            { new(7,0), new(12,"天地否","乾上坤下") },

            { new(6,7), new(9,"风天小畜","巽上乾下") },
            { new(6,6), new(0,"","") },
            { new(6,5), new(0,"","") },
            { new(6,4), new(0,"","") },
            { new(6,3), new(0,"","") },
            { new(6,2), new(0,"","") },
            { new(6,1), new(0,"","") },
            { new(6,0), new(0,"","") },

            { new(5,7), new(14,"火天大有","离上乾下") },
            { new(5,6), new(0,"","") },
            { new(5,5), new(0,"","") },
            { new(5,4), new(0,"","") },
            { new(5,3), new(0,"","") },
            { new(5,2), new(0,"","") },
            { new(5,1), new(0,"","") },
            { new(5,0), new(0,"","") },

            { new(4,7), new(0,"","") },
            { new(4,6), new(0,"","") },
            { new(4,5), new(0,"","") },
            { new(4,4), new(0,"","") },
            { new(4,3), new(0,"","") },
            { new(4,2), new(4, "山水蒙", "艮上坎下") },
            { new(4,1), new(0,"","") },
            { new(4,0), new(0,"","") },

            { new(3,7), new(0,"","") },
            { new(3,6), new(0,"","") },
            { new(3,5), new(0,"","") },
            { new(3,4), new(0,"","") },
            { new(3,3), new(0,"","") },
            { new(3,2), new(0,"","") },
            { new(3,1), new(17,"泽雷随","兑上震下") },
            { new(3,0), new(0,"","") },

            { new(2,7), new(5, "水天需", "坎上乾下") },
            { new(2,6), new(0,"","") },
            { new(2,5), new(0,"","") },
            { new(2,4), new(0,"","") },
            { new(2,3), new(0,"","") },
            { new(2,2), new(0,"","") },
            { new(2,1), new(3, "水雷屯","坎上震下") },
            { new(2,0), new(8, "水地比","坎上坤下") },

            { new(1,7), new(0,"","") },
            { new(1,6), new(0,"","") },
            { new(1,5), new(0,"","") },
            { new(1,4), new(0,"","") },
            { new(1,3), new(0,"","") },
            { new(1,2), new(0,"","") },
            { new(1,1), new(0,"","") },
            { new(1,0), new(16,"雷地豫","震上坤下") },

            { new(0,7), new(11,"地天泰","坤上乾下") },
            { new(0,6), new(0,"","") },
            { new(0,5), new(0,"","") },
            { new(0,4), new(15,"地山谦","坤上艮下") },
            { new(0,3), new(0,"","") },
            { new(0,2), new(7,"地水师","坤上坎下") },
            { new(0,1), new(0,"","") },
            { new(0,0), new(2, "坤为地","坤上坤下") },
        };
        }

        public static int GetDT12(DateTime dateTime)
        {
            var HH = dateTime.Hour;
            return GetDT12(HH);
        }
        /*
         * 子时（23-01）
         * 丑时（01-03）
         * 寅时（03-05）
         * 卯时（05-07）
         * 辰时（07-09）
         * 巳时（09-11）
         * 午时（11-13）
         * 未时（13-15）
         * 申时（15-17）
         * 酉时（17-19）
         * 戌时（19-21）
         * 亥时（21-23） 
         * */
        public static int GetDT12(int HH) =>
            (((HH & 1) > 0 ? (HH + 1) : HH) % 24 / 2) + 1;
        public static string DT12Description { get => "子时（23-01）// 丑时（01-03）// 寅时（03-05）// 卯时（05-07）// 辰时（07-09）// 巳时（09-11）// 午时（11-13）// 未时（13-15）// 申时（15-17）// 酉时（17-19）// 戌时（19-21）// 亥时（21-23）"; }
        public static int BeginRsl(int sh, int dn) => Dics[sh & Full3Positive] << 3 | Dics[dn & Full3Positive];
        public static int ChangedRsl(int begin, int rateNum) => begin ^ BitNums[rateNum % 6];
        public static int ExchangeRsl(int begin, int change)
        {
            int src = begin;
            if (begin == 0 || begin == Full6Positive) src = change;
            return (src >> 2 & Full3Positive) << 3 | (src >> 1 & Full3Positive);
        }
    }

    public class SingleSymbol
    {
        public SingleSymbol(int symNum, string name, string direct, string sym)
        {
            SymbolNum = symNum;
            Name = name;
            Direct = direct;
            Symbol = sym;
        }
        public int SymbolNum { get; protected set; }
        public string Name { get; protected set; }
        public string Direct { get; protected set; }
        public string Symbol { get; protected set; }
    }
}