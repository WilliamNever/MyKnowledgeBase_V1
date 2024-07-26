namespace Fast8Calculting.Forms
{
    partial class UscHHhourConvert
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
            drpListHH = new ComboBox();
            btnConvert = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // drpListHH
            // 
            drpListHH.BackColor = SystemColors.Window;
            drpListHH.DropDownStyle = ComboBoxStyle.DropDownList;
            drpListHH.FlatStyle = FlatStyle.Flat;
            drpListHH.FormattingEnabled = true;
            drpListHH.Location = new Point(76, 6);
            drpListHH.Name = "drpListHH";
            drpListHH.Size = new Size(109, 23);
            drpListHH.TabIndex = 0;
            // 
            // btnConvert
            // 
            btnConvert.Location = new Point(200, 6);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(75, 23);
            btnConvert.TabIndex = 1;
            btnConvert.Text = "Convert";
            btnConvert.UseVisualStyleBackColor = true;
            btnConvert.Click += btnConvert_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 10);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 2;
            label1.Text = "Select HH -";
            // 
            // UscHHhourConvert
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(btnConvert);
            Controls.Add(drpListHH);
            Name = "UscHHhourConvert";
            Size = new Size(285, 37);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox drpListHH;
        private Button btnConvert;
        private Label label1;
    }
}
