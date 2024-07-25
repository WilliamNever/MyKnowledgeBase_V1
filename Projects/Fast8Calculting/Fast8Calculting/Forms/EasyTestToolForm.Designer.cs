namespace Fast8Calculting.Forms
{
    partial class EasyTestToolForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlBackGround = new Panel();
            grp8GuaName = new GroupBox();
            grpEasyTesting = new GroupBox();
            txtRslt = new TextBox();
            grpDTConverter = new GroupBox();
            usrcBasicInput = new UscBasicEasyInput();
            pnlBackGround.SuspendLayout();
            grpEasyTesting.SuspendLayout();
            grpDTConverter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBackGround
            // 
            pnlBackGround.Controls.Add(grp8GuaName);
            pnlBackGround.Controls.Add(grpEasyTesting);
            pnlBackGround.Controls.Add(grpDTConverter);
            pnlBackGround.Dock = DockStyle.Fill;
            pnlBackGround.Location = new Point(0, 0);
            pnlBackGround.Name = "pnlBackGround";
            pnlBackGround.Size = new Size(814, 511);
            pnlBackGround.TabIndex = 0;
            // 
            // grp8GuaName
            // 
            grp8GuaName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            grp8GuaName.Location = new Point(488, 12);
            grp8GuaName.Name = "grp8GuaName";
            grp8GuaName.Size = new Size(314, 169);
            grp8GuaName.TabIndex = 2;
            grp8GuaName.TabStop = false;
            grp8GuaName.Text = "8 Name";
            // 
            // grpEasyTesting
            // 
            grpEasyTesting.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpEasyTesting.Controls.Add(txtRslt);
            grpEasyTesting.Location = new Point(12, 187);
            grpEasyTesting.Name = "grpEasyTesting";
            grpEasyTesting.Size = new Size(790, 312);
            grpEasyTesting.TabIndex = 1;
            grpEasyTesting.TabStop = false;
            grpEasyTesting.Text = "Easy Testing";
            // 
            // txtRslt
            // 
            txtRslt.Dock = DockStyle.Fill;
            txtRslt.Location = new Point(3, 19);
            txtRslt.Multiline = true;
            txtRslt.Name = "txtRslt";
            txtRslt.ScrollBars = ScrollBars.Vertical;
            txtRslt.Size = new Size(784, 290);
            txtRslt.TabIndex = 0;
            // 
            // grpDTConverter
            // 
            grpDTConverter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpDTConverter.Controls.Add(usrcBasicInput);
            grpDTConverter.Location = new Point(12, 12);
            grpDTConverter.Name = "grpDTConverter";
            grpDTConverter.Size = new Size(470, 169);
            grpDTConverter.TabIndex = 0;
            grpDTConverter.TabStop = false;
            grpDTConverter.Text = "Date time converter";
            // 
            // usrcBasicInput
            // 
            usrcBasicInput.Dock = DockStyle.Fill;
            usrcBasicInput.Location = new Point(3, 19);
            usrcBasicInput.Name = "usrcBasicInput";
            usrcBasicInput.Size = new Size(464, 147);
            usrcBasicInput.TabIndex = 0;
            // 
            // EasyTestToolForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(814, 511);
            Controls.Add(pnlBackGround);
            MinimumSize = new Size(830, 550);
            Name = "EasyTestToolForm";
            Text = "Easy Test Tool Form";
            FormClosing += EasyTestToolForm_FormClosing;
            pnlBackGround.ResumeLayout(false);
            grpEasyTesting.ResumeLayout(false);
            grpEasyTesting.PerformLayout();
            grpDTConverter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlBackGround;
        private GroupBox grpEasyTesting;
        private GroupBox grpDTConverter;
        private GroupBox grp8GuaName;
        private TextBox txtRslt;
        private UscBasicEasyInput usrcBasicInput;
    }
}