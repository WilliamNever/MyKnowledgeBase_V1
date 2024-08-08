using F8C.Core.Consts;
using F8C.Core.Interfaces;
using F8C.Core.Models;
using F8C.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fast8Calculting.Forms
{
    public partial class UscBasicEasyInput : UserControl
    {
        protected IRenderUIInterface? render;
        protected Control[] YaoValidGrp;
        protected Control[] NumListValidGrp;
        public void SetRender(IRenderUIInterface Render)
        {
            render = Render;
        }
        public UscBasicEasyInput()
        {
            InitializeComponent();

            YaoValidGrp = new Control[] { txtUpper, txtLower, txtChgRate };
            NumListValidGrp = new Control[] { txtInpuNums };
            ReSetCausesValidation(YaoValidGrp, false);
            ReSetCausesValidation(NumListValidGrp, false);
#if debug
            txtUpper.Text = "14";
            txtLower.Text = "56";
            txtChgRate.Text = "70";
            txtDes.Text = "For testing in 2024-07-25 14:56:00";
#endif
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            ReSetCausesValidation(YaoValidGrp, true);
            var isv = ValidateChildren(ValidationConstraints.Enabled);
            ReSetCausesValidation(YaoValidGrp, false);
            if (isv)
            {
                var service = AppHost.GetSerivce<PlumEasyCalculateService>();
                var model = new SimpleModel
                {
                    First = int.Parse(txtUpper.Text),
                    Second = int.Parse(txtLower.Text),
                    RateNum = int.Parse(txtChgRate.Text),
                };
                Result8SymbolModel rsl = service.GetPlumEasyCalculate8Symbol(model);

                var nLine = Environment.NewLine;
                StringBuilder sb = new StringBuilder();
                sb.Append($"Description -");
                sb.Append($"{nLine}{nLine}");
                sb.Append($"{txtDes.Text}");
                sb.Append($"{nLine}{nLine}");
                sb.Append($"Numbers Created by - {model.First}/{model.Second} ");
                sb.Append($"{nLine}");
                sb.Append($"Active number - {model.RateNum}");
                sb.Append($"{nLine}{nLine}");
                sb.Append($"Result - Begin / Exchange / Destination");
                sb.Append($"{nLine}");
                for (int i = 5; i > -1; i--)
                {
                    var chFlag = i == (rsl.ChangedYao - 1) ? " x " : "   ";
                    sb.Append($"{GetYao(rsl.Begin, i)} {chFlag}\t\t");
                    sb.Append($"{GetYao(rsl.Exchange, i)}\t\t");
                    sb.Append($"{GetYao(rsl.Destination, i)}\t\t");
                    sb.Append($"{nLine}");
                }
                sb.Append($"{nLine}{nLine}");
                sb.Append("Name - Begin (num Name symbol) / Destination (num Name symbol)");
                sb.Append($"{nLine}");
                var strt = ConstDefine.GuaName[rsl.Begin];
                var des = ConstDefine.GuaName[rsl.Destination];
                sb.Append($"{strt.Num} {strt.Name} {strt.SymbolName}");
                sb.Append($"{nLine}");
                sb.Append($"{des.Num} {des.Name} {des.SymbolName}");
                sb.Append($"{nLine}{nLine}");
                render?.RenderUI(sb.ToString());
            }
        }

        private string GetYao(GuaModel gua, int rMoved)
        {
            var str = ((gua.Gua >> rMoved) & 1) > 0 ? ConstDefine.Positive : ConstDefine.Negative;
            return $"{str}";
        }

        private void IntValidating(object sender, CancelEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
            {
                if (!int.TryParse(tb.Text, out _))
                {
                    e.Cancel = true;
                    tb.BackColor = Color.Red;
                }
                else
                {
                    tb.BackColor = Color.White;
                }
            }
        }
        private void IntListValidating(object sender, CancelEventArgs e)
        {
            var tb = sender as TextBox;
            Regex reg = new Regex(@"^([ ]*[\d]*[ ]*" + Environment.NewLine + ")*$");
            if (tb != null)
            {
                if (!reg.IsMatch(tb.Text + Environment.NewLine))
                {
                    e.Cancel = true;
                    tb.BackColor = Color.Red;
                }
                else
                {
                    tb.BackColor = Color.White;
                }
            }
        }

        private void ReSetCausesValidation(Control[] controls, bool canCause)
        {
            foreach (Control c in controls)
            {
                c.CausesValidation = canCause;
                if (c.HasChildren)
                {
                    ReSetCausesValidation(c.Controls.OfType<Control>().ToArray(), canCause);
                }
            }
        }

        private void btnSum_Click(object sender, EventArgs e)
        {
            try
            {
                ReSetCausesValidation(NumListValidGrp, true);
                var isv = ValidateChildren(ValidationConstraints.Enabled);
                ReSetCausesValidation(NumListValidGrp, false);
                if (isv && !string.IsNullOrEmpty(txtInpuNums.Text.Trim()))
                {
                    var Nums = txtInpuNums.Text.Split(Environment.NewLine,
                        StringSplitOptions.RemoveEmptyEntries
                        | StringSplitOptions.TrimEntries)
                        .Select(i => int.Parse(i)).ToArray();

                    txtChgRate.Text = ConstDefine.Sum(Nums).ToString();
                }
            }
            catch (Exception ex) {
                txtInpuNums.BackColor = Color.Red;
                MessageBox.Show(this, ex.Message);
            }
        }
    }
}