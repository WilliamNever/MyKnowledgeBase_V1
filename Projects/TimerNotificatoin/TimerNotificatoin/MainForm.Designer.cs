namespace TimerNotificatoin
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            nfyTimer = new NotifyIcon(components);
            cmsIcon = new ContextMenuStrip(components);
            tmiOpenOrHiden = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            tmiExit = new ToolStripMenuItem();
            pnlBackGround = new Panel();
            sstInfor = new StatusStrip();
            tslStatus = new ToolStripStatusLabel();
            btnStop = new Button();
            btnStart = new Button();
            cmsIcon.SuspendLayout();
            pnlBackGround.SuspendLayout();
            sstInfor.SuspendLayout();
            SuspendLayout();
            // 
            // nfyTimer
            // 
            nfyTimer.ContextMenuStrip = cmsIcon;
            nfyTimer.Icon = (Icon)resources.GetObject("nfyTimer.Icon");
            nfyTimer.Text = "Notification Timer";
            nfyTimer.Visible = true;
            nfyTimer.DoubleClick += nfyTimer_DoubleClick;
            // 
            // cmsIcon
            // 
            cmsIcon.Items.AddRange(new ToolStripItem[] { tmiOpenOrHiden, toolStripSeparator1, tmiExit });
            cmsIcon.Name = "cmsIcon";
            cmsIcon.Size = new Size(104, 54);
            cmsIcon.ItemClicked += cmsIcon_ItemClicked;
            // 
            // tmiOpenOrHiden
            // 
            tmiOpenOrHiden.Name = "tmiOpenOrHiden";
            tmiOpenOrHiden.Size = new Size(103, 22);
            tmiOpenOrHiden.Text = "Open";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(100, 6);
            // 
            // tmiExit
            // 
            tmiExit.Name = "tmiExit";
            tmiExit.Size = new Size(103, 22);
            tmiExit.Text = "Exit";
            // 
            // pnlBackGround
            // 
            pnlBackGround.Controls.Add(sstInfor);
            pnlBackGround.Controls.Add(btnStop);
            pnlBackGround.Controls.Add(btnStart);
            pnlBackGround.Dock = DockStyle.Fill;
            pnlBackGround.Location = new Point(0, 0);
            pnlBackGround.Name = "pnlBackGround";
            pnlBackGround.Size = new Size(800, 450);
            pnlBackGround.TabIndex = 1;
            // 
            // sstInfor
            // 
            sstInfor.Items.AddRange(new ToolStripItem[] { tslStatus });
            sstInfor.Location = new Point(0, 428);
            sstInfor.Name = "sstInfor";
            sstInfor.Size = new Size(800, 22);
            sstInfor.TabIndex = 3;
            sstInfor.Text = "statusStrip1";
            // 
            // tslStatus
            // 
            tslStatus.Name = "tslStatus";
            tslStatus.Size = new Size(42, 17);
            tslStatus.Text = "Ready!";
            // 
            // btnStop
            // 
            btnStop.Location = new Point(671, 370);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(97, 23);
            btnStop.TabIndex = 2;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(548, 370);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(97, 23);
            btnStart.TabIndex = 1;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlBackGround);
            Name = "MainForm";
            Text = "Main";
            SizeChanged += MainForm_SizeChanged;
            cmsIcon.ResumeLayout(false);
            pnlBackGround.ResumeLayout(false);
            pnlBackGround.PerformLayout();
            sstInfor.ResumeLayout(false);
            sstInfor.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon nfyTimer;
        private ContextMenuStrip cmsIcon;
        private ToolStripMenuItem tmiOpenOrHiden;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem tmiExit;
        private Panel pnlBackGround;
        private Button btnStart;
        private Button btnStop;
        private StatusStrip sstInfor;
        private ToolStripStatusLabel tslStatus;
    }
}