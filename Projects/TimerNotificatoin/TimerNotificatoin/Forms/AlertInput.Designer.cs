namespace TimerNotificatoin.Forms
{
    partial class AlertInput
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
            pnlBackGrd = new Panel();
            grpBoxLooper = new GroupBox();
            cbkHasEndDate = new CheckBox();
            btnShowDetails = new Button();
            dlLoopTemplates = new ComboBox();
            dtpEndOfDate = new DateTimePicker();
            label7 = new Label();
            cbNType = new ComboBox();
            label5 = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            txtDescription = new TextBox();
            label4 = new Label();
            txtTitle = new TextBox();
            label3 = new Label();
            cbAlert = new CheckBox();
            label2 = new Label();
            label1 = new Label();
            dtPicker = new DateTimePicker();
            pnlBackGrd.SuspendLayout();
            grpBoxLooper.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBackGrd
            // 
            pnlBackGrd.Controls.Add(grpBoxLooper);
            pnlBackGrd.Controls.Add(cbNType);
            pnlBackGrd.Controls.Add(label5);
            pnlBackGrd.Controls.Add(btnSave);
            pnlBackGrd.Controls.Add(btnCancel);
            pnlBackGrd.Controls.Add(txtDescription);
            pnlBackGrd.Controls.Add(label4);
            pnlBackGrd.Controls.Add(txtTitle);
            pnlBackGrd.Controls.Add(label3);
            pnlBackGrd.Controls.Add(cbAlert);
            pnlBackGrd.Controls.Add(label2);
            pnlBackGrd.Controls.Add(label1);
            pnlBackGrd.Controls.Add(dtPicker);
            pnlBackGrd.Dock = DockStyle.Fill;
            pnlBackGrd.Location = new Point(0, 0);
            pnlBackGrd.Name = "pnlBackGrd";
            pnlBackGrd.Size = new Size(614, 436);
            pnlBackGrd.TabIndex = 0;
            // 
            // grpBoxLooper
            // 
            grpBoxLooper.Controls.Add(cbkHasEndDate);
            grpBoxLooper.Controls.Add(btnShowDetails);
            grpBoxLooper.Controls.Add(dlLoopTemplates);
            grpBoxLooper.Controls.Add(dtpEndOfDate);
            grpBoxLooper.Controls.Add(label7);
            grpBoxLooper.Location = new Point(324, 11);
            grpBoxLooper.Name = "grpBoxLooper";
            grpBoxLooper.Size = new Size(267, 125);
            grpBoxLooper.TabIndex = 12;
            grpBoxLooper.TabStop = false;
            grpBoxLooper.Text = "Loop";
            // 
            // cbkHasEndDate
            // 
            cbkHasEndDate.AutoSize = true;
            cbkHasEndDate.Checked = true;
            cbkHasEndDate.CheckState = CheckState.Checked;
            cbkHasEndDate.Location = new Point(6, 27);
            cbkHasEndDate.Name = "cbkHasEndDate";
            cbkHasEndDate.RightToLeft = RightToLeft.Yes;
            cbkHasEndDate.Size = new Size(73, 19);
            cbkHasEndDate.TabIndex = 15;
            cbkHasEndDate.Text = "End Date";
            cbkHasEndDate.TextAlign = ContentAlignment.MiddleCenter;
            cbkHasEndDate.UseVisualStyleBackColor = true;
            cbkHasEndDate.CheckedChanged += cbkHasEndDate_CheckedChanged;
            // 
            // btnShowDetails
            // 
            btnShowDetails.Location = new Point(200, 88);
            btnShowDetails.Name = "btnShowDetails";
            btnShowDetails.Size = new Size(61, 23);
            btnShowDetails.TabIndex = 14;
            btnShowDetails.Text = "Details";
            btnShowDetails.UseVisualStyleBackColor = true;
            btnShowDetails.Click += btnShowDetails_Click;
            // 
            // dlLoopTemplates
            // 
            dlLoopTemplates.DropDownStyle = ComboBoxStyle.DropDownList;
            dlLoopTemplates.FormattingEnabled = true;
            dlLoopTemplates.Location = new Point(6, 88);
            dlLoopTemplates.Name = "dlLoopTemplates";
            dlLoopTemplates.Size = new Size(188, 23);
            dlLoopTemplates.TabIndex = 13;
            // 
            // dtpEndOfDate
            // 
            dtpEndOfDate.CalendarFont = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dtpEndOfDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpEndOfDate.Format = DateTimePickerFormat.Custom;
            dtpEndOfDate.Location = new Point(89, 25);
            dtpEndOfDate.Name = "dtpEndOfDate";
            dtpEndOfDate.Size = new Size(172, 23);
            dtpEndOfDate.TabIndex = 2;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 63);
            label7.Name = "label7";
            label7.Size = new Size(102, 15);
            label7.TabIndex = 1;
            label7.Text = "Loop Templates - ";
            // 
            // cbNType
            // 
            cbNType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbNType.FormattingEnabled = true;
            cbNType.Location = new Point(117, 11);
            cbNType.Name = "cbNType";
            cbNType.Size = new Size(200, 23);
            cbNType.TabIndex = 11;
            cbNType.SelectedIndexChanged += cbNType_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 15);
            label5.Name = "label5";
            label5.Size = new Size(68, 15);
            label5.TabIndex = 10;
            label5.Text = "Alert Type -";
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.DialogResult = DialogResult.OK;
            btnSave.Location = new Point(517, 398);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 9;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(15, 398);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtDescription
            // 
            txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.Location = new Point(117, 158);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(474, 234);
            txtDescription.TabIndex = 7;
            txtDescription.Validating += txtRequired_Validating;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 158);
            label4.Name = "label4";
            label4.Size = new Size(75, 15);
            label4.TabIndex = 6;
            label4.Text = "Description -";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(117, 113);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(200, 23);
            txtTitle.TabIndex = 5;
            txtTitle.Validating += txtRequired_Validating;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 117);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 4;
            label3.Text = "Title -";
            // 
            // cbAlert
            // 
            cbAlert.AutoSize = true;
            cbAlert.Checked = true;
            cbAlert.CheckState = CheckState.Checked;
            cbAlert.Location = new Point(117, 85);
            cbAlert.Name = "cbAlert";
            cbAlert.Size = new Size(15, 14);
            cbAlert.TabIndex = 3;
            cbAlert.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 85);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 2;
            label2.Text = "Alert -";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 52);
            label1.Name = "label1";
            label1.Size = new Size(97, 15);
            label1.TabIndex = 1;
            label1.Text = "Alert Date Time -";
            // 
            // dtPicker
            // 
            dtPicker.CalendarFont = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dtPicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtPicker.Format = DateTimePickerFormat.Custom;
            dtPicker.Location = new Point(117, 48);
            dtPicker.Name = "dtPicker";
            dtPicker.Size = new Size(200, 23);
            dtPicker.TabIndex = 0;
            // 
            // AlertInput
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(614, 436);
            Controls.Add(pnlBackGrd);
            MinimizeBox = false;
            MinimumSize = new Size(630, 475);
            Name = "AlertInput";
            Text = "Notification input";
            FormClosing += AlertInput_FormClosing;
            pnlBackGrd.ResumeLayout(false);
            pnlBackGrd.PerformLayout();
            grpBoxLooper.ResumeLayout(false);
            grpBoxLooper.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlBackGrd;
        private DateTimePicker dtPicker;
        private Label label1;
        private CheckBox cbAlert;
        private Label label2;
        private Label label4;
        private TextBox txtTitle;
        private Label label3;
        private TextBox txtDescription;
        private Button btnSave;
        private Button btnCancel;
        private ComboBox cbNType;
        private Label label5;
        private GroupBox grpBoxLooper;
        private DateTimePicker dtpEndOfDate;
        private Label label7;
        private ComboBox dlLoopTemplates;
        private Button btnShowDetails;
        private CheckBox cbkHasEndDate;
    }
}