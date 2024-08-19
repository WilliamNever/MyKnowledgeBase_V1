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

        public void RenderUI(string str, string? barMessage = null)
        {
            Invoke(() =>
            {
                txtRslt.Text = str;
                var barLabel = stbStatus.Items.Find(stblMessage.Name, false).FirstOrDefault();
                if (barLabel != null)
                {
                    barLabel.Text = $"{DateTime.Now} - {barMessage ?? "Updated Content ..."}";
                }
            });
        }
        public void RenderUIStatusBar(string? barMessage = null)
        {
            Invoke(() =>
            {
                var barLabel = stbStatus.Items.Find(stblMessage.Name, false).FirstOrDefault();
                if (barLabel != null)
                {
                    barLabel.Text = $"{DateTime.Now} - {barMessage ?? "Updated Content ..."}";
                }
            });
        }

        private void EasyTestToolForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtRslt.Text = "";
            RenderUIStatusBar($"Cleared!");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var dt = DateTime.Now;
            SaveFileDialog svf = new SaveFileDialog();
            svf.Title = "Save the File";
            svf.Filter = "Txt|*.txt|*.*|*.*";
            svf.InitialDirectory = Environment.CurrentDirectory;
            //svf.CheckFileExists = true;
            svf.CheckPathExists = true;
            svf.FileName = $"{dt.Year}-{dt.Month.ToString().PadLeft(2, '0')}-{dt.Day.ToString().PadLeft(2, '0')}_Ask_";
            if (svf.ShowDialog() == DialogResult.OK)
            {
                var fp = svf.FileName;
                using var fs = new FileStream(fp, FileMode.Create, FileAccess.Write);
                using var sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(txtRslt.Text);
                sw.Flush();
                RenderUIStatusBar($"Save to {Path.GetFileName(svf.FileName)}");
            }
        }
    }
}