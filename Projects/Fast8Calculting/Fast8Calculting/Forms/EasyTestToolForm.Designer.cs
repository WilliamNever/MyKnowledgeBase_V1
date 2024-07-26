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
            stbStatus = new StatusStrip();
            stblMessage = new ToolStripStatusLabel();
            grp8GuaName = new GroupBox();
            uscGuaNameSearcher = new UscGuaNameSearch();
            uscHHConverter = new UscHHhourConvert();
            grpEasyTesting = new GroupBox();
            btnClear = new Button();
            btnSave = new Button();
            txtRslt = new TextBox();
            grpDTConverter = new GroupBox();
            usrcBasicInput = new UscBasicEasyInput();
            menuMain = new MenuStrip();
            pnlBackGround.SuspendLayout();
            stbStatus.SuspendLayout();
            grp8GuaName.SuspendLayout();
            grpEasyTesting.SuspendLayout();
            grpDTConverter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBackGround
            // 
            pnlBackGround.Controls.Add(stbStatus);
            pnlBackGround.Controls.Add(grp8GuaName);
            pnlBackGround.Controls.Add(grpEasyTesting);
            pnlBackGround.Controls.Add(grpDTConverter);
            pnlBackGround.Controls.Add(menuMain);
            pnlBackGround.Dock = DockStyle.Fill;
            pnlBackGround.Location = new Point(0, 0);
            pnlBackGround.Name = "pnlBackGround";
            pnlBackGround.Size = new Size(814, 541);
            pnlBackGround.TabIndex = 0;
            // 
            // stbStatus
            // 
            stbStatus.Items.AddRange(new ToolStripItem[] { stblMessage });
            stbStatus.Location = new Point(0, 519);
            stbStatus.Name = "stbStatus";
            stbStatus.Size = new Size(814, 22);
            stbStatus.TabIndex = 4;
            stbStatus.Text = "statusStrip1";
            // 
            // stblMessage
            // 
            stblMessage.Name = "stblMessage";
            stblMessage.Size = new Size(0, 17);
            // 
            // grp8GuaName
            // 
            grp8GuaName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            grp8GuaName.Controls.Add(uscGuaNameSearcher);
            grp8GuaName.Controls.Add(uscHHConverter);
            grp8GuaName.Location = new Point(488, 32);
            grp8GuaName.Name = "grp8GuaName";
            grp8GuaName.Size = new Size(314, 190);
            grp8GuaName.TabIndex = 2;
            grp8GuaName.TabStop = false;
            grp8GuaName.Text = "8 Name";
            // 
            // uscGuaNameSearcher
            // 
            uscGuaNameSearcher.Location = new Point(6, 66);
            uscGuaNameSearcher.Name = "uscGuaNameSearcher";
            uscGuaNameSearcher.Size = new Size(302, 113);
            uscGuaNameSearcher.TabIndex = 1;
            // 
            // uscHHConverter
            // 
            uscHHConverter.Location = new Point(6, 19);
            uscHHConverter.Name = "uscHHConverter";
            uscHHConverter.Size = new Size(302, 36);
            uscHHConverter.TabIndex = 0;
            // 
            // grpEasyTesting
            // 
            grpEasyTesting.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpEasyTesting.Controls.Add(btnClear);
            grpEasyTesting.Controls.Add(btnSave);
            grpEasyTesting.Controls.Add(txtRslt);
            grpEasyTesting.Location = new Point(12, 228);
            grpEasyTesting.Name = "grpEasyTesting";
            grpEasyTesting.Size = new Size(790, 288);
            grpEasyTesting.TabIndex = 1;
            grpEasyTesting.TabStop = false;
            grpEasyTesting.Text = "Easy Testing";
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClear.Location = new Point(619, 259);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 2;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(712, 259);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtRslt
            // 
            txtRslt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtRslt.Location = new Point(3, 19);
            txtRslt.Multiline = true;
            txtRslt.Name = "txtRslt";
            txtRslt.ScrollBars = ScrollBars.Vertical;
            txtRslt.Size = new Size(784, 233);
            txtRslt.TabIndex = 0;
            // 
            // grpDTConverter
            // 
            grpDTConverter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpDTConverter.Controls.Add(usrcBasicInput);
            grpDTConverter.Location = new Point(12, 32);
            grpDTConverter.Name = "grpDTConverter";
            grpDTConverter.Size = new Size(470, 190);
            grpDTConverter.TabIndex = 0;
            grpDTConverter.TabStop = false;
            grpDTConverter.Text = "Date time converter";
            // 
            // usrcBasicInput
            // 
            usrcBasicInput.CausesValidation = false;
            usrcBasicInput.Dock = DockStyle.Fill;
            usrcBasicInput.Location = new Point(3, 19);
            usrcBasicInput.Name = "usrcBasicInput";
            usrcBasicInput.Size = new Size(464, 168);
            usrcBasicInput.TabIndex = 0;
            // 
            // menuMain
            // 
            menuMain.Location = new Point(0, 0);
            menuMain.Name = "menuMain";
            menuMain.Size = new Size(814, 24);
            menuMain.TabIndex = 3;
            menuMain.Text = "menuStrip1";
            // 
            // EasyTestToolForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(814, 541);
            Controls.Add(pnlBackGround);
            MainMenuStrip = menuMain;
            MinimumSize = new Size(830, 580);
            Name = "EasyTestToolForm";
            Text = "Easy Test Tool Form";
            FormClosing += EasyTestToolForm_FormClosing;
            pnlBackGround.ResumeLayout(false);
            pnlBackGround.PerformLayout();
            stbStatus.ResumeLayout(false);
            stbStatus.PerformLayout();
            grp8GuaName.ResumeLayout(false);
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
        private Button btnClear;
        private Button btnSave;
        private MenuStrip menuMain;
        private StatusStrip stbStatus;
        private ToolStripStatusLabel stblMessage;
        private UscHHhourConvert uscHHConverter;
        private UscGuaNameSearch uscGuaNameSearcher;
    }
}