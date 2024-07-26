namespace Fast8Calculting
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
            pnlBackGround = new Panel();
            ssbStatBar = new StatusStrip();
            ssbMessage = new ToolStripStatusLabel();
            menuMain = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            easyTestToolStripMenuItem = new ToolStripMenuItem();
            pnlBackGround.SuspendLayout();
            ssbStatBar.SuspendLayout();
            menuMain.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBackGround
            // 
            pnlBackGround.Controls.Add(ssbStatBar);
            pnlBackGround.Controls.Add(menuMain);
            pnlBackGround.Dock = DockStyle.Fill;
            pnlBackGround.Location = new Point(0, 0);
            pnlBackGround.Name = "pnlBackGround";
            pnlBackGround.Size = new Size(800, 450);
            pnlBackGround.TabIndex = 2;
            // 
            // ssbStatBar
            // 
            ssbStatBar.Items.AddRange(new ToolStripItem[] { ssbMessage });
            ssbStatBar.Location = new Point(0, 428);
            ssbStatBar.Name = "ssbStatBar";
            ssbStatBar.Size = new Size(800, 22);
            ssbStatBar.TabIndex = 2;
            ssbStatBar.Text = "statusStrip1";
            // 
            // ssbMessage
            // 
            ssbMessage.Name = "ssbMessage";
            ssbMessage.Size = new Size(0, 17);
            // 
            // menuMain
            // 
            menuMain.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, aboutToolStripMenuItem });
            menuMain.Location = new Point(0, 0);
            menuMain.Name = "menuMain";
            menuMain.Size = new Size(800, 24);
            menuMain.TabIndex = 1;
            menuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            fileToolStripMenuItem.DropDownItemClicked += MenuDropDownItemsClicked;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.AccessibleName = "OpenFile";
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(126, 22);
            openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.AccessibleName = "SaveFile";
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(126, 22);
            saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.AccessibleName = "SaveAs";
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(126, 22);
            saveAsToolStripMenuItem.Text = "Save As ...";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { easyTestToolStripMenuItem });
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(52, 20);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.DropDownItemClicked += MenuDropDownItemsClicked;
            // 
            // easyTestToolStripMenuItem
            // 
            easyTestToolStripMenuItem.AccessibleName = "EasyTestTool";
            easyTestToolStripMenuItem.Name = "easyTestToolStripMenuItem";
            easyTestToolStripMenuItem.Size = new Size(120, 22);
            easyTestToolStripMenuItem.Text = "Easy Test Tool";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlBackGround);
            Name = "MainForm";
            Text = "Main Center";
            pnlBackGround.ResumeLayout(false);
            pnlBackGround.PerformLayout();
            ssbStatBar.ResumeLayout(false);
            ssbStatBar.PerformLayout();
            menuMain.ResumeLayout(false);
            menuMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlBackGround;
        private StatusStrip ssbStatBar;
        private ToolStripStatusLabel ssbMessage;
        private MenuStrip menuMain;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem easyTestToolStripMenuItem;
    }
}