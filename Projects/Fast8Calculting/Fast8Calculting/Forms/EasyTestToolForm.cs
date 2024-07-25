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
    public partial class EasyTestToolForm : Form, IRenderUIInterface
    {
        public EasyTestToolForm()
        {
            InitializeComponent();
            usrcBasicInput.SetRender(this);
        }

        public void RenderUI(string str)
        {
            Invoke(() =>
            {
                txtRslt.Text = str;
            });
        }

        private void EasyTestToolForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }
    }
}
