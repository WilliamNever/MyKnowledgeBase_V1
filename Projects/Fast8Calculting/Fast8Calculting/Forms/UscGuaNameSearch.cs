﻿using F8C.Core.Consts;
using F8C.Core.Interfaces;
using F8C.Core.Models;
using System.Data;

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
    }
}