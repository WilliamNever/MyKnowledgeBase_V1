using F8C.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            uscHHConverter.SetRender(this);
            uscGuaNameSearcher.SetRender(this);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtRslt.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf = new SaveFileDialog();
            svf.Title = "Save the File";
            svf.Filter = "Txt|*.txt|*.*|*.*";
            svf.InitialDirectory = Environment.CurrentDirectory;
            //svf.CheckFileExists = true;
            svf.CheckPathExists = true;
            if (svf.ShowDialog() == DialogResult.OK)
            {
                var fp = svf.FileName;
                using var fs = new FileStream(fp, FileMode.Create, FileAccess.Write);
                using var sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(txtRslt.Text);
                sw.Flush();
            }
        }
    }
}
