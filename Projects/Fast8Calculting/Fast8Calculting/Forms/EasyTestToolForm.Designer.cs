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
            btnClear = new Button();
            btnSave = new Button();
            txtRslt = new TextBox();
            grpDTConverter = new GroupBox();
            usrcBasicInput = new UscBasicEasyInput();
            menuMain = new MenuStrip();
            stbStatus = new StatusStrip();
            stblMessage = new ToolStripStatusLabel();
            pnlBackGround.SuspendLayout();
            grpEasyTesting.SuspendLayout();
            grpDTConverter.SuspendLayout();
            stbStatus.SuspendLayout();
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
            pnlBackGround.Size = new Size(814, 531);
            pnlBackGround.TabIndex = 0;
            // 
            // grp8GuaName
            // 
            grp8GuaName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            grp8GuaName.Location = new Point(488, 32);
            grp8GuaName.Name = "grp8GuaName";
            grp8GuaName.Size = new Size(314, 156);
            grp8GuaName.TabIndex = 2;
            grp8GuaName.TabStop = false;
            grp8GuaName.Text = "8 Name";
            // 
            // grpEasyTesting
            // 
            grpEasyTesting.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpEasyTesting.Controls.Add(btnClear);
            grpEasyTesting.Controls.Add(btnSave);
            grpEasyTesting.Controls.Add(txtRslt);
            grpEasyTesting.Location = new Point(12, 194);
            grpEasyTesting.Name = "grpEasyTesting";
            grpEasyTesting.Size = new Size(790, 312);
            grpEasyTesting.TabIndex = 1;
            grpEasyTesting.TabStop = false;
            grpEasyTesting.Text = "Easy Testing";
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClear.Location = new Point(619, 283);
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
            btnSave.Location = new Point(712, 283);
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
            txtRslt.Size = new Size(784, 257);
            txtRslt.TabIndex = 0;
            // 
            // grpDTConverter
            // 
            grpDTConverter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpDTConverter.Controls.Add(usrcBasicInput);
            grpDTConverter.Location = new Point(12, 32);
            grpDTConverter.Name = "grpDTConverter";
            grpDTConverter.Size = new Size(470, 156);
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
            usrcBasicInput.Size = new Size(464, 134);
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
            // stbStatus
            // 
            stbStatus.Items.AddRange(new ToolStripItem[] { stblMessage });
            stbStatus.Location = new Point(0, 509);
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
            // EasyTestToolForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(814, 531);
            Controls.Add(pnlBackGround);
            MainMenuStrip = menuMain;
            MinimumSize = new Size(830, 570);
            Name = "EasyTestToolForm";
            Text = "Easy Test Tool Form";
            FormClosing += EasyTestToolForm_FormClosing;
            pnlBackGround.ResumeLayout(false);
            pnlBackGround.PerformLayout();
            grpEasyTesting.ResumeLayout(false);
            grpEasyTesting.PerformLayout();
            grpDTConverter.ResumeLayout(false);
            stbStatus.ResumeLayout(false);
            stbStatus.PerformLayout();
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
    }
}