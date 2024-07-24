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
            grpDTConverter = new GroupBox();
            pnlBackGround.SuspendLayout();
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
            pnlBackGround.Size = new Size(814, 461);
            pnlBackGround.TabIndex = 0;
            // 
            // grp8GuaName
            // 
            grp8GuaName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            grp8GuaName.Location = new Point(488, 12);
            grp8GuaName.Name = "grp8GuaName";
            grp8GuaName.Size = new Size(314, 124);
            grp8GuaName.TabIndex = 2;
            grp8GuaName.TabStop = false;
            grp8GuaName.Text = "8 Name";
            // 
            // grpEasyTesting
            // 
            grpEasyTesting.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpEasyTesting.Location = new Point(12, 142);
            grpEasyTesting.Name = "grpEasyTesting";
            grpEasyTesting.Size = new Size(790, 307);
            grpEasyTesting.TabIndex = 1;
            grpEasyTesting.TabStop = false;
            grpEasyTesting.Text = "Easy Testing";
            // 
            // grpDTConverter
            // 
            grpDTConverter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpDTConverter.Location = new Point(12, 12);
            grpDTConverter.Name = "grpDTConverter";
            grpDTConverter.Size = new Size(470, 124);
            grpDTConverter.TabIndex = 0;
            grpDTConverter.TabStop = false;
            grpDTConverter.Text = "Date time converter";
            // 
            // EasyTestToolForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(814, 461);
            Controls.Add(pnlBackGround);
            MinimumSize = new Size(830, 500);
            Name = "EasyTestToolForm";
            Text = "Easy Test Tool Form";
            pnlBackGround.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlBackGround;
        private GroupBox grpEasyTesting;
        private GroupBox grpDTConverter;
        private GroupBox grp8GuaName;
    }
}