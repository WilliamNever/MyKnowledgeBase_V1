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
            grpEasyTesting = new GroupBox();
            grpDTConverter = new GroupBox();
            pnlBackGround.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBackGround
            // 
            pnlBackGround.Controls.Add(grpEasyTesting);
            pnlBackGround.Controls.Add(grpDTConverter);
            pnlBackGround.Dock = DockStyle.Fill;
            pnlBackGround.Location = new Point(0, 0);
            pnlBackGround.Name = "pnlBackGround";
            pnlBackGround.Size = new Size(800, 450);
            pnlBackGround.TabIndex = 0;
            // 
            // grpEasyTesting
            // 
            grpEasyTesting.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpEasyTesting.Location = new Point(12, 132);
            grpEasyTesting.Name = "grpEasyTesting";
            grpEasyTesting.Size = new Size(776, 306);
            grpEasyTesting.TabIndex = 1;
            grpEasyTesting.TabStop = false;
            grpEasyTesting.Text = "Easy Testing";
            // 
            // grpDTConverter
            // 
            grpDTConverter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpDTConverter.Location = new Point(12, 12);
            grpDTConverter.Name = "grpDTConverter";
            grpDTConverter.Size = new Size(776, 100);
            grpDTConverter.TabIndex = 0;
            grpDTConverter.TabStop = false;
            grpDTConverter.Text = "Date time converter";
            // 
            // EasyTestToolForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlBackGround);
            Name = "EasyTestToolForm";
            Text = "Easy Test Tool Form";
            pnlBackGround.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlBackGround;
        private GroupBox grpEasyTesting;
        private GroupBox grpDTConverter;
    }
}