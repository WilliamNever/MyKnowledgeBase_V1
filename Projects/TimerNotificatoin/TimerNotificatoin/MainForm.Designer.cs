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
            dgDataList = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Title = new DataGridViewTextBoxColumn();
            AlertDateTime = new DataGridViewTextBoxColumn();
            IsAlerted = new DataGridViewTextBoxColumn();
            nfyTimer = new NotifyIcon(components);
            cmsIcon = new ContextMenuStrip(components);
            tmiOpenOrHiden = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            tmiExit = new ToolStripMenuItem();
            pnlBackGround = new Panel();
            btnDelete = new Button();
            btnAddAlert = new Button();
            sstInfor = new StatusStrip();
            tslStatus = new ToolStripStatusLabel();
            mnuMain = new MenuStrip();
            tmiHelp = new ToolStripMenuItem();
            tsdmHelperFile = new ToolStripMenuItem();
            btnStop = new Button();
            btnStart = new Button();
            ((System.ComponentModel.ISupportInitialize)dgDataList).BeginInit();
            cmsIcon.SuspendLayout();
            pnlBackGround.SuspendLayout();
            sstInfor.SuspendLayout();
            mnuMain.SuspendLayout();
            SuspendLayout();
            // 
            // dgDataList
            // 
            dgDataList.AllowUserToOrderColumns = true;
            dgDataList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgDataList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgDataList.Columns.AddRange(new DataGridViewColumn[] { Id, Title, AlertDateTime, IsAlerted });
            dgDataList.Location = new Point(12, 39);
            dgDataList.Name = "dgDataList";
            dgDataList.ReadOnly = true;
            dgDataList.RowTemplate.Height = 25;
            dgDataList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgDataList.Size = new Size(776, 335);
            dgDataList.TabIndex = 4;
            dgDataList.RowHeaderMouseDoubleClick += dgDataList_RowHeaderMouseDoubleClick;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.ReadOnly = true;
            // 
            // Title
            // 
            Title.DataPropertyName = "Title";
            Title.HeaderText = "Title";
            Title.Name = "Title";
            Title.ReadOnly = true;
            // 
            // AlertDateTime
            // 
            AlertDateTime.DataPropertyName = "AlertDateTime";
            AlertDateTime.HeaderText = "Alert Date Time";
            AlertDateTime.Name = "AlertDateTime";
            AlertDateTime.ReadOnly = true;
            // 
            // IsAlerted
            // 
            IsAlerted.DataPropertyName = "IsAlerted";
            IsAlerted.HeaderText = "Is Alerted";
            IsAlerted.Name = "IsAlerted";
            IsAlerted.ReadOnly = true;
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
            pnlBackGround.Controls.Add(btnDelete);
            pnlBackGround.Controls.Add(btnAddAlert);
            pnlBackGround.Controls.Add(dgDataList);
            pnlBackGround.Controls.Add(sstInfor);
            pnlBackGround.Controls.Add(mnuMain);
            pnlBackGround.Controls.Add(btnStop);
            pnlBackGround.Controls.Add(btnStart);
            pnlBackGround.Dock = DockStyle.Fill;
            pnlBackGround.Location = new Point(0, 0);
            pnlBackGround.Name = "pnlBackGround";
            pnlBackGround.Size = new Size(800, 450);
            pnlBackGround.TabIndex = 1;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Location = new Point(115, 380);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(97, 23);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnAddAlert
            // 
            btnAddAlert.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAddAlert.Location = new Point(12, 380);
            btnAddAlert.Name = "btnAddAlert";
            btnAddAlert.Size = new Size(97, 23);
            btnAddAlert.TabIndex = 6;
            btnAddAlert.Text = "Add";
            btnAddAlert.UseVisualStyleBackColor = true;
            btnAddAlert.Click += btnAddAlert_Click;
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
            // mnuMain
            // 
            mnuMain.Items.AddRange(new ToolStripItem[] { tmiHelp });
            mnuMain.Location = new Point(0, 0);
            mnuMain.Name = "mnuMain";
            mnuMain.Size = new Size(800, 24);
            mnuMain.TabIndex = 5;
            mnuMain.Text = "menuStrip1";
            // 
            // tmiHelp
            // 
            tmiHelp.DropDownItems.AddRange(new ToolStripItem[] { tsdmHelperFile });
            tmiHelp.Name = "tmiHelp";
            tmiHelp.Size = new Size(44, 20);
            tmiHelp.Text = "Help";
            tmiHelp.DropDownItemClicked += DropDownItemClicked;
            // 
            // tsdmHelperFile
            // 
            tsdmHelperFile.AccessibleName = "HelperFile";
            tsdmHelperFile.Name = "tsdmHelperFile";
            tsdmHelperFile.Size = new Size(180, 22);
            tsdmHelperFile.Text = "Helper File";
            // 
            // btnStop
            // 
            btnStop.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnStop.Location = new Point(691, 397);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(97, 23);
            btnStop.TabIndex = 2;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnStart.Location = new Point(588, 397);
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
            MainMenuStrip = mnuMain;
            Name = "MainForm";
            Text = "Main";
            SizeChanged += MainForm_SizeChanged;
            ((System.ComponentModel.ISupportInitialize)dgDataList).EndInit();
            cmsIcon.ResumeLayout(false);
            pnlBackGround.ResumeLayout(false);
            pnlBackGround.PerformLayout();
            sstInfor.ResumeLayout(false);
            sstInfor.PerformLayout();
            mnuMain.ResumeLayout(false);
            mnuMain.PerformLayout();
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
        private DataGridView dgDataList;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn AlertDateTime;
        private DataGridViewTextBoxColumn IsAlerted;
        private MenuStrip mnuMain;
        private Button btnDelete;
        private Button btnAddAlert;
        private ToolStripMenuItem tmiHelp;
        private ToolStripMenuItem tsdmHelperFile;
    }
}