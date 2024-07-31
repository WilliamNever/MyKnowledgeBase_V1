using F8C.Core.Models;

namespace F8C.Core.Consts
{
    public class ConstDefine
    {
        public const string Positive = "\u2500\u2500\u2500\u2500";
        public const string Negative = "\u2500\u0020\u0020\u2500";

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
                { 7, new SingleSymbol(1,"乾","西北","天", "金") } ,
                { 3, new SingleSymbol(2,"兑","西","泽", "金") } ,
                { 5, new SingleSymbol(3,"离","南","火", "火") } ,
                { 1, new SingleSymbol(4,"震","东","雷", "木") } ,

                { 6, new SingleSymbol(5,"巽","东南","风", "木") } ,
                { 2, new SingleSymbol(6,"坎","北","水", "水") } ,
                { 4, new SingleSymbol(7,"艮","东北","山", "土") } ,
                { 0, new SingleSymbol(8,"坤","西南","地", "土") } ,

            };

        public static Dictionary<GuaModel, GuaNameModel> GuaName
        {
            get => new() {
            { new(7,7), new(1,"乾为天","乾上乾下") },
            { new(7,6), new(44,"天风姤","乾上巽下") },
            { new(7,5), new(13,"天火同人","乾上离下") },
            { new(7,4), new(33,"天山遁","乾上艮下") },
            { new(7,3), new(10,"天泽履","乾上兑下") },
            { new(7,2), new(6,"天水讼","乾上坎下") },
            { new(7,1), new(25,"天雷无妄","乾上震下") },
            { new(7,0), new(12,"天地否","乾上坤下") },

            { new(6,7), new(9,"风天小畜","巽上乾下") },
            { new(6,6), new(57,"巽为风","巽上巽下") },
            { new(6,5), new(37,"风火家人","巽上离下") },
            { new(6,4), new(53,"风山渐","巽上艮下") },
            { new(6,3), new(61,"风泽中孚","巽上兑下") },
            { new(6,2), new(59,"风水涣","巽上坎下") },
            { new(6,1), new(42,"风雷益","巽上震下") },
            { new(6,0), new(20,"风地观","巽上坤下") },

            { new(5,7), new(14,"火天大有","离上乾下") },
            { new(5,6), new(50,"火风鼎","离上巽下") },
            { new(5,5), new(30,"离为火","离上离下") },
            { new(5,4), new(56,"火山旅","离上艮下") },
            { new(5,3), new(38,"火泽睽","离上兑下") },
            { new(5,2), new(64,"火水未济","离上坎下") },
            { new(5,1), new(21,"火雷噬嗑","离上震下") },
            { new(5,0), new(35,"火地晋","离上坤下") },

            { new(4,7), new(26,"山天大畜","艮上乾下") },
            { new(4,6), new(18,"山风蛊","艮上巽下") },
            { new(4,5), new(22,"山火贲","艮上离下") },
            { new(4,4), new(52,"艮为山","艮上艮下") },
            { new(4,3), new(41,"山泽损","艮上兑下") },
            { new(4,2), new(4, "山水蒙", "艮上坎下") },
            { new(4,1), new(27,"山雷颐","艮上震下") },
            { new(4,0), new(23,"山地剥","艮上坤下") },

            { new(3,7), new(43,"泽天夬","兑上乾下") },
            { new(3,6), new(28,"泽风大过","兑上巽下") },
            { new(3,5), new(49,"泽火革","兑上离下") },
            { new(3,4), new(31,"泽山咸","兑上艮下") },
            { new(3,3), new(58,"兑为泽","兑上兑下") },
            { new(3,2), new(47,"泽水困","兑上坎下") },
            { new(3,1), new(17,"泽雷随","兑上震下") },
            { new(3,0), new(45,"泽地萃","兑上坤下") },

            { new(2,7), new(5, "水天需", "坎上乾下") },
            { new(2,6), new(48,"水风井","坎上巽下") },
            { new(2,5), new(63,"水火既济","坎上离下") },
            { new(2,4), new(39,"水山蹇","坎上艮下") },
            { new(2,3), new(60,"水泽节","坎上兑下") },
            { new(2,2), new(29,"坎为水","坎上坎下") },
            { new(2,1), new(3, "水雷屯","坎上震下") },
            { new(2,0), new(8, "水地比","坎上坤下") },

            { new(1,7), new(34,"雷天大壮","震上乾下") },
            { new(1,6), new(32,"雷风恒","震上巽下") },
            { new(1,5), new(55,"雷火丰","震上离下") },
            { new(1,4), new(62,"雷山小过","震上艮下") },
            { new(1,3), new(54,"雷泽归妹","震上兑下") },
            { new(1,2), new(40,"雷水解","震上坎下") },
            { new(1,1), new(51,"震为雷","震上震下") },
            { new(1,0), new(16,"雷地豫","震上坤下") },

            { new(0,7), new(11,"地天泰","坤上乾下") },
            { new(0,6), new(46,"地风升","坤上巽下") },
            { new(0,5), new(36,"地火明夷","坤上离下") },
            { new(0,4), new(15,"地山谦","坤上艮下") },
            { new(0,3), new(19,"地泽临","坤上兑下") },
            { new(0,2), new(7,"地水师","坤上坎下") },
            { new(0,1), new(24,"地雷复","坤上震下") },
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

        #region DT12 nameList
        public static string DT12Description { get => "子时 (水鼠)（23 ->◦ 01）// 丑时 (土牛)（01 ->◦ 03）// 寅时 (木虎)（03 ->◦ 05）// 卯时 (木兔)（05 ->◦ 07）// 辰时 (土龙)（07 ->◦ 09）// 巳时 (火蛇)（09 ->◦ 11）// 午时 (火马)（11 ->◦ 13）// 未时 (土羊)（13 ->◦ 15）// 申时 (金猴)（15 ->◦ 17）// 酉时 (金鸡)（17 ->◦ 19）// 戌时 (土犬)（19 ->◦ 21）// 亥时 (水猪)（21 ->◦ 23）"; }
        #endregion

        public static int BeginRsl(int sh, int dn) => Dics[sh & Full3Positive] << 3 | Dics[dn & Full3Positive];
        public static int ChangedRsl(int begin, int rateNum) => begin ^ BitNums[rateNum % 6];
        public static int GetChangeYao(int rateNum) => rateNum % 6;
        public static int ExchangeRsl(int begin, int change)
        {
            int src = begin;
            if (begin == 0 || begin == Full6Positive) src = change;
            return (src >> 2 & Full3Positive) << 3 | (src >> 1 & Full3Positive);
        }
    }

    public class SingleSymbol
    {
        public SingleSymbol(int symNum, string name, string direct, string sym, string wuXin)
        {
            SymbolNum = symNum;
            Name = name;
            Direct = direct;
            Symbol = sym;
            WuXin = wuXin;

        }
        public int SymbolNum { get; protected set; }
        public string Name { get; protected set; }
        public string Direct { get; protected set; }
        public string Symbol { get; protected set; }
        public string WuXin { get; protected set; }
    }
}