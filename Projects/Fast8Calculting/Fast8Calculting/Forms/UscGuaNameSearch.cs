using F8C.Core.Consts;
using F8C.Core.Interfaces;
using F8C.Core.Models;
using System.Data;
using System.Text;

namespace Fast8Calculting.Forms
{
    public partial class UscGuaNameSearch : UserControl
    {
        protected IRenderUIInterface? render;
        public void SetRender(IRenderUIInterface Render)
        {
            render = Render;
        }

        public UscGuaNameSearch()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            var array = ConstDefine.DicName.OrderBy(x => x.Value.SymbolNum)
                .Select(x => new SimpleKeyValueItem(
                    $"{x.Value.Name}/SymbVal-{x.Value.SymbolNum}" +
                    $"/{x.Value.Direct}/{x.Value.Symbol}/{x.Value.WuXin}/Val-{x.Key}",
                    x.Key)).ToArray();

            cmbUpper.Items.AddRange(array);
            cmbUpper.SelectedIndex = 0;
            cmbLower.Items.AddRange(array);
            cmbLower.SelectedIndex = 0;

            cmbUpper.DisplayMember = "key";
            cmbUpper.ValueMember = "value";
            cmbLower.DisplayMember = "key";
            cmbLower.ValueMember = "value";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var up = ((SimpleKeyValueItem)cmbUpper.SelectedItem).value;
            var low = ((SimpleKeyValueItem)cmbLower.SelectedItem).value;
            GuaModel guaIndex = new GuaModel(up, low);
            var gua = ConstDefine.GuaName[guaIndex];
            render?.RenderUI($"Num.{gua.Num} Name - {gua.Name} / {gua.SymbolName}");
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new("");

            #region dt reflect
            sb.Append($"\t\t\t\t午{Environment.NewLine}");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"\t\t\t巳\t\t未{Environment.NewLine}");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"\t\t辰\t巽4\t离9\t坤2\t申{Environment.NewLine}");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"\t卯\t\t震3\t5\t兑7\t\t酉{Environment.NewLine}");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"\t\t寅\t艮8\t坎1\t乾6\t戌{Environment.NewLine}");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"\t\t\t丑\t\t亥{Environment.NewLine}");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"\t\t\t\t子{Environment.NewLine}");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"{Environment.NewLine}");
            #endregion

            #region 3 yuan 9 yun
            sb.Append($"上元");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"一运1864年--1883年(同治三年甲子至光绪九年癸未)");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"二运1884年--1903年(光绪十年甲申至光绪廿九年癸卯)");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"三运1904年--1923年(光绪卅年甲辰至民国十二年癸亥)");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"中元");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"四运1924年--1943年(民国十三年甲子至卅二年癸未)");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"五运1944年--1963年(民国卅三年甲申至1963年癸卯)");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"六运1964年--1983年(1964年甲辰至1983年癸亥)");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"下元");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"七运1984年--2003年(1984年甲子至2003年癸未年)");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"八运2004年--2023年(2004年甲申至2023年癸卯年)");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"九运2024年--2043年(2024年甲辰至2043年癸亥年)");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"{Environment.NewLine}");

            sb.Append($"九运【离】2024年--2043年九宫");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"846\r\n792\r\n351");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"{Environment.NewLine}");
            sb.Append($"{Environment.NewLine}");
            #endregion

            render?.RenderUI(sb.ToString());
        }
    }
}
