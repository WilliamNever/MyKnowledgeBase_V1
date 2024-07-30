using F8C.Core.Consts;
using F8C.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fast8Calculting.Forms
{
    public record SimpleKeyValueItem(string key, int value);
    public partial class UscHHhourConvert : UserControl
    {
        protected IRenderUIInterface? render;
        public void SetRender(IRenderUIInterface Render)
        {
            render = Render;
        }
        public UscHHhourConvert()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            var items = new List<int>();
            for (int i = 0; i < 24; i++) { items.Add(i); }
            drpListHH.Items.AddRange(
                items.Select(x => new SimpleKeyValueItem(x.ToString(), x)).ToArray());
            drpListHH.DisplayMember = "key";
            drpListHH.ValueMember = "value";
            drpListHH.SelectedIndex = 0;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            var descs = ConstDefine.DT12Description.Split("//"
                , StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var HH = ((SimpleKeyValueItem)drpListHH.SelectedItem).value;
            var HH12 = ConstDefine.GetDT12(HH);
            render?.RenderUI($"{descs[HH12 - 1]} - {HH12}{Environment.NewLine}{Environment.NewLine}{string.Join(Environment.NewLine, descs)}");
        }
    }
}
