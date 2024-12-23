﻿namespace TimerNotificatoin
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
            nfyTimer = new NotifyIcon(components);
            cmsIcon = new ContextMenuStrip(components);
            tmiStartOrStop = new ToolStripMenuItem();
            tmiOpenOrHiden = new ToolStripMenuItem();
            tmiNotifyConfigFolder = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            tmiExit = new ToolStripMenuItem();
            pnlBackGround = new Panel();
            btnSaveAlerts = new Button();
            btnReloadAlerts = new Button();
            btnDelete = new Button();
            btnAddAlert = new Button();
            sstInfor = new StatusStrip();
            tslStatus = new ToolStripStatusLabel();
            mnuMain = new MenuStrip();
            tmiFiles = new ToolStripMenuItem();
            tsmShowNotificationsFoler = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            tsdmExit = new ToolStripMenuItem();
            tmiEdit = new ToolStripMenuItem();
            tsmDefaultSort = new ToolStripMenuItem();
            tmiHelp = new ToolStripMenuItem();
            tsdmHelperFile = new ToolStripMenuItem();
            btnStop = new Button();
            btnStart = new Button();
            OrderIndex = new DataGridViewTextBoxColumn();
            Id = new DataGridViewTextBoxColumn();
            ClassificationID = new DataGridViewTextBoxColumn();
            NotificationType = new DataGridViewTextBoxColumn();
            ClassificationName = new DataGridViewTextBoxColumn();
            Title = new DataGridViewTextBoxColumn();
            AlertDateTime = new DataGridViewTextBoxColumn();
            ToAlert = new DataGridViewTextBoxColumn();
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
            dgDataList.Columns.AddRange(new DataGridViewColumn[] { OrderIndex, Id, ClassificationID, NotificationType, ClassificationName, Title, AlertDateTime, ToAlert });
            dgDataList.Location = new Point(12, 39);
            dgDataList.Name = "dgDataList";
            dgDataList.ReadOnly = true;
            dgDataList.RowTemplate.Height = 25;
            dgDataList.Size = new Size(776, 335);
            dgDataList.TabIndex = 4;
            dgDataList.CellDoubleClick += dgDataList_CellDoubleClick;
            dgDataList.ColumnHeaderMouseDoubleClick += dgDataList_ColumnHeaderMouseDoubleClick;
            dgDataList.RowHeaderMouseDoubleClick += dgDataList_RowHeaderMouseDoubleClick;
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
            cmsIcon.Items.AddRange(new ToolStripItem[] { tmiStartOrStop, tmiOpenOrHiden, tmiNotifyConfigFolder, toolStripSeparator1, tmiExit });
            cmsIcon.Name = "cmsIcon";
            cmsIcon.Size = new Size(174, 98);
            cmsIcon.Opening += cmsIcon_Opening;
            cmsIcon.ItemClicked += cmsIcon_ItemClicked;
            // 
            // tmiStartOrStop
            // 
            tmiStartOrStop.Name = "tmiStartOrStop";
            tmiStartOrStop.Size = new Size(173, 22);
            tmiStartOrStop.Text = "Start";
            // 
            // tmiOpenOrHiden
            // 
            tmiOpenOrHiden.Name = "tmiOpenOrHiden";
            tmiOpenOrHiden.Size = new Size(173, 22);
            tmiOpenOrHiden.Text = "Open";
            // 
            // tmiNotifyConfigFolder
            // 
            tmiNotifyConfigFolder.Name = "tmiNotifyConfigFolder";
            tmiNotifyConfigFolder.Size = new Size(173, 22);
            tmiNotifyConfigFolder.Text = "Notification Folder";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(170, 6);
            // 
            // tmiExit
            // 
            tmiExit.Name = "tmiExit";
            tmiExit.Size = new Size(173, 22);
            tmiExit.Text = "Exit";
            // 
            // pnlBackGround
            // 
            pnlBackGround.Controls.Add(btnSaveAlerts);
            pnlBackGround.Controls.Add(btnReloadAlerts);
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
            // btnSaveAlerts
            // 
            btnSaveAlerts.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSaveAlerts.Location = new Point(352, 380);
            btnSaveAlerts.Name = "btnSaveAlerts";
            btnSaveAlerts.Size = new Size(97, 23);
            btnSaveAlerts.TabIndex = 9;
            btnSaveAlerts.Text = "Save Alerts";
            btnSaveAlerts.UseVisualStyleBackColor = true;
            btnSaveAlerts.Click += btnSaveAlerts_Click;
            // 
            // btnReloadAlerts
            // 
            btnReloadAlerts.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnReloadAlerts.Location = new Point(249, 380);
            btnReloadAlerts.Name = "btnReloadAlerts";
            btnReloadAlerts.Size = new Size(97, 23);
            btnReloadAlerts.TabIndex = 8;
            btnReloadAlerts.Text = "Reload Alerts";
            btnReloadAlerts.UseVisualStyleBackColor = true;
            btnReloadAlerts.Click += btnReloadAlerts_Click;
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
            mnuMain.Items.AddRange(new ToolStripItem[] { tmiFiles, tmiEdit, tmiHelp });
            mnuMain.Location = new Point(0, 0);
            mnuMain.Name = "mnuMain";
            mnuMain.Size = new Size(800, 24);
            mnuMain.TabIndex = 5;
            mnuMain.Text = "menuStrip1";
            // 
            // tmiFiles
            // 
            tmiFiles.DropDownItems.AddRange(new ToolStripItem[] { tsmShowNotificationsFoler, toolStripSeparator2, tsdmExit });
            tmiFiles.Name = "tmiFiles";
            tmiFiles.Size = new Size(42, 20);
            tmiFiles.Text = "Files";
            tmiFiles.DropDownItemClicked += DropDownItemClicked;
            // 
            // tsmShowNotificationsFoler
            // 
            tsmShowNotificationsFoler.AccessibleName = "ShowNotificationsFoler";
            tsmShowNotificationsFoler.Name = "tsmShowNotificationsFoler";
            tsmShowNotificationsFoler.Size = new Size(203, 22);
            tsmShowNotificationsFoler.Text = "Show Notifications Foler";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(200, 6);
            // 
            // tsdmExit
            // 
            tsdmExit.AccessibleName = "Exit";
            tsdmExit.Name = "tsdmExit";
            tsdmExit.Size = new Size(203, 22);
            tsdmExit.Text = "Exit";
            // 
            // tmiEdit
            // 
            tmiEdit.DropDownItems.AddRange(new ToolStripItem[] { tsmDefaultSort });
            tmiEdit.Name = "tmiEdit";
            tmiEdit.Size = new Size(39, 20);
            tmiEdit.Text = "Edit";
            tmiEdit.DropDownItemClicked += DropDownItemClicked;
            // 
            // tsmDefaultSort
            // 
            tsmDefaultSort.AccessibleName = "DefaultSort";
            tsmDefaultSort.Name = "tsmDefaultSort";
            tsmDefaultSort.Size = new Size(136, 22);
            tsmDefaultSort.Text = "Default Sort";
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
            tsdmHelperFile.Size = new Size(130, 22);
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
            btnStop.Click += btnStop_Click;
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
            // OrderIndex
            // 
            OrderIndex.Frozen = true;
            OrderIndex.HeaderText = "Index";
            OrderIndex.Name = "OrderIndex";
            OrderIndex.ReadOnly = true;
            OrderIndex.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.SortMode = DataGridViewColumnSortMode.Programmatic;
            Id.Visible = false;
            // 
            // ClassificationID
            // 
            ClassificationID.DataPropertyName = "ClassificationID";
            ClassificationID.HeaderText = "ClassificationID";
            ClassificationID.Name = "ClassificationID";
            ClassificationID.ReadOnly = true;
            ClassificationID.Visible = false;
            // 
            // NotificationType
            // 
            NotificationType.DataPropertyName = "NotificationType";
            NotificationType.HeaderText = "Notification Type";
            NotificationType.Name = "NotificationType";
            NotificationType.ReadOnly = true;
            NotificationType.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // ClassificationName
            // 
            ClassificationName.DataPropertyName = "ClassificationName";
            ClassificationName.HeaderText = "Classification";
            ClassificationName.Name = "ClassificationName";
            ClassificationName.ReadOnly = true;
            ClassificationName.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // Title
            // 
            Title.DataPropertyName = "Title";
            Title.HeaderText = "Title";
            Title.Name = "Title";
            Title.ReadOnly = true;
            Title.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // AlertDateTime
            // 
            AlertDateTime.HeaderText = "Alert Date Time";
            AlertDateTime.Name = "AlertDateTime";
            AlertDateTime.ReadOnly = true;
            AlertDateTime.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // ToAlert
            // 
            ToAlert.DataPropertyName = "ToAlert";
            ToAlert.HeaderText = "To Alert";
            ToAlert.Name = "ToAlert";
            ToAlert.ReadOnly = true;
            ToAlert.SortMode = DataGridViewColumnSortMode.Programmatic;
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
            FormClosing += MainForm_FormClosing;
            SizeChanged += MainForm_SizeChanged;
            Paint += MainForm_Paint;
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
        private MenuStrip mnuMain;
        private Button btnDelete;
        private Button btnAddAlert;
        private ToolStripMenuItem tmiHelp;
        private ToolStripMenuItem tsdmHelperFile;
        private ToolStripMenuItem tmiStartOrStop;
        private Button btnReloadAlerts;
        private Button btnSaveAlerts;
        private ToolStripMenuItem tmiFiles;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem tsdmExit;
        private ToolStripMenuItem tsmShowNotificationsFoler;
        private ToolStripMenuItem tmiEdit;
        private ToolStripMenuItem tsmDefaultSort;
        private ToolStripMenuItem tmiNotifyConfigFolder;
        private DataGridViewTextBoxColumn OrderIndex;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn ClassificationID;
        private DataGridViewTextBoxColumn NotificationType;
        private DataGridViewTextBoxColumn ClassificationName;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn AlertDateTime;
        private DataGridViewTextBoxColumn ToAlert;
    }
}