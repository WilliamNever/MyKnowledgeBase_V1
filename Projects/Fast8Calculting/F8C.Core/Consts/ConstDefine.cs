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
        public static readonly Dictionary<int, SingleSymbol> DicName
            = new()
            {
                { 0, new SingleSymbol(0,"坤","西南","地") } ,
                { 7, new SingleSymbol(7,"乾","西北","天") } ,
                { 3, new SingleSymbol(3,"兑","西","泽") } ,
                { 5, new SingleSymbol(5,"离","南","火") } ,

                { 1, new SingleSymbol(1,"震","东","雷") } ,
                { 6, new SingleSymbol(6,"巽","东南","风") } ,
                { 2, new SingleSymbol(2,"坎","北","水") } ,
                { 4, new SingleSymbol(4,"艮","东北","山") } ,
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