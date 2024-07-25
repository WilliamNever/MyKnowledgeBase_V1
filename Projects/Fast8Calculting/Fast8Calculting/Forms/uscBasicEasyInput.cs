using F8C.Core.Interfaces;
using F8C.Infrastructure.Services;
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
    public partial class UscBasicEasyInput : UserControl
    {
        protected IRenderUIInterface render;
        public void SetRender(IRenderUIInterface Render)
        {
            render = Render;
        }
        public UscBasicEasyInput()
        {
            InitializeComponent();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            var service = AppHost.GetSerivce<PlumEasyCalculateService>();
            render.RenderUI("OK");
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
    }
}