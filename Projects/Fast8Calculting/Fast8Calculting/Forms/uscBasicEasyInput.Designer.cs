namespace Fast8Calculting.Forms
{
    partial class UscBasicEasyInput
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtUpper = new TextBox();
            txtLower = new TextBox();
            txtChgRate = new TextBox();
            btnCalc = new Button();
            label4 = new Label();
            txtDes = new TextBox();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 11);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 0;
            label1.Text = "Upper - ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 43);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 1;
            label2.Text = "Lower - ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 74);
            label3.Name = "label3";
            label3.Size = new Size(63, 15);
            label3.TabIndex = 2;
            label3.Text = "ChgRate - ";
            // 
            // txtUpper
            // 
            txtUpper.Location = new Point(78, 7);
            txtUpper.Name = "txtUpper";
            txtUpper.Size = new Size(131, 23);
            txtUpper.TabIndex = 3;
            txtUpper.Validating += IntValidating;
            // 
            // txtLower
            // 
            txtLower.Location = new Point(78, 39);
            txtLower.Name = "txtLower";
            txtLower.Size = new Size(131, 23);
            txtLower.TabIndex = 4;
            txtLower.Validating += IntValidating;
            // 
            // txtChgRate
            // 
            txtChgRate.Location = new Point(78, 70);
            txtChgRate.Name = "txtChgRate";
            txtChgRate.Size = new Size(131, 23);
            txtChgRate.TabIndex = 5;
            txtChgRate.Validating += IntValidating;
            // 
            // btnCalc
            // 
            btnCalc.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCalc.Location = new Point(7, 115);
            btnCalc.Name = "btnCalc";
            btnCalc.Size = new Size(75, 23);
            btnCalc.TabIndex = 6;
            btnCalc.Text = "Calc";
            btnCalc.UseVisualStyleBackColor = true;
            btnCalc.Click += btnCalc_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(232, 7);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 7;
            label4.Text = "Description - ";
            // 
            // txtDes
            // 
            txtDes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtDes.Location = new Point(232, 28);
            txtDes.Multiline = true;
            txtDes.Name = "txtDes";
            txtDes.ScrollBars = ScrollBars.Vertical;
            txtDes.Size = new Size(369, 110);
            txtDes.TabIndex = 8;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnCalc);
            panel1.Controls.Add(txtChgRate);
            panel1.Controls.Add(txtDes);
            panel1.Controls.Add(txtLower);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(txtUpper);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(612, 145);
            panel1.TabIndex = 9;
            // 
            // UscBasicEasyInput
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "UscBasicEasyInput";
            Size = new Size(612, 146);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtUpper;
        private TextBox txtLower;
        private TextBox txtChgRate;
        private Button btnCalc;
        private Label label4;
        private TextBox txtDes;
        private Panel panel1;
    }
}
