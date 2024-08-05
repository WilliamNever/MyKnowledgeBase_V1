namespace Fast8Calculting.Forms
{
    partial class UscGuaNameSearch
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSearch = new Button();
            cmbUpper = new ComboBox();
            cmbLower = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            btnLoc = new Button();
            SuspendLayout();
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(205, 81);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // cmbUpper
            // 
            cmbUpper.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUpper.FlatStyle = FlatStyle.Flat;
            cmbUpper.FormattingEnabled = true;
            cmbUpper.Location = new Point(63, 8);
            cmbUpper.Name = "cmbUpper";
            cmbUpper.Size = new Size(217, 23);
            cmbUpper.TabIndex = 1;
            // 
            // cmbLower
            // 
            cmbLower.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLower.FlatStyle = FlatStyle.Flat;
            cmbLower.FormattingEnabled = true;
            cmbLower.Location = new Point(63, 45);
            cmbLower.Name = "cmbLower";
            cmbLower.Size = new Size(217, 23);
            cmbLower.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 12);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 3;
            label1.Text = "Upper - ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 49);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 4;
            label2.Text = "Lower - ";
            // 
            // btnLoc
            // 
            btnLoc.Location = new Point(3, 81);
            btnLoc.Name = "btnLoc";
            btnLoc.Size = new Size(75, 23);
            btnLoc.TabIndex = 5;
            btnLoc.Text = "Location12";
            btnLoc.UseVisualStyleBackColor = true;
            btnLoc.Click += btnLoc_Click;
            // 
            // UscGuaNameSearch
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnLoc);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cmbLower);
            Controls.Add(cmbUpper);
            Controls.Add(btnSearch);
            Name = "UscGuaNameSearch";
            Size = new Size(293, 111);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSearch;
        private ComboBox cmbUpper;
        private ComboBox cmbLower;
        private Label label1;
        private Label label2;
        private Button btnLoc;
    }
}
